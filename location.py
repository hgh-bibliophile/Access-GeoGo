import pyodbc
import sys
import re
import requests
import os 

try:
    
    if len(sys.argv) > 1:
        path = sys.argv[1]
    else: 
        print("No database path given")
        sys.exit()
    if path is not None and not os.path.exists(path) and '.accdb' in path:
        print("Not a valid Acess database")
        sys.exit()

    gpsRegex = re.compile(r"[-+]?([1-8]?\d(\.\d+)?|90(\.0+)?),\s*[-+]?(180(\.0+)?|((1[0-7]\d)|([1-9]?\d))(\.\d+)?)")
    conn = pyodbc.connect(r'Driver={Microsoft Access Driver (*.mdb, *.accdb)};DBQ=' + path + ';')
    cursor = conn.cursor()
    cursor.execute('select [Merchant_ID], [Link] from Merchant WHERE [Latitude] is NULL;')
    
    updated = 0
    for row in cursor.fetchall():
        if row[1] is not None and 'http' in row[1]:
            shorturl = row[1]
            longurl = requests.get(shorturl,  headers = {'User-agent': 'Fuel Station GPS Locations'}).url
            gps = gpsRegex.search(longurl)
            print(row)
            print (shorturl + " -> " + longurl)
            if gps is not None:
                lat = float(gps.group().split(",")[0])
                long = float(gps.group().split(",")[1])
                cursor.execute(f'Update [Merchant] Set [Latitude] = {lat:.6f}, [Longitude] = {long:.6f} Where [Merchant_ID] = {row[0]};')
                updated+=1
                print (gps.group())
            else:
                print ("No GPS Coords")
            print()
    conn.commit()
    conn.close()
    print(f'{updated} Locations Updated')
    
except KeyboardInterrupt:
    conn.commit()
    conn.close()
    print(f'{updated} Locations Updated')
    sys.exit()
