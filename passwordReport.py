# Script to create a report of when passwords were last revised in the vault
        
import subprocess
import json
import csv
        
# setup
session_key = input('Input session key: ') # bw session key
org_id = input('Input Org ID: ') # org ID
bw = ['./bw', '--session', session_key]
        
with open('report.csv', 'w', newline='') as csvfile:
  # define a writer function to write to .csv files
  reportWriter = csv.writer(csvfile, delimiter = ',', quotechar=' ', quoting=csv.QUOTE_MINIMAL)
        
  # get all items + orgID flag from CLI tool
  items = subprocess.run(bw + ['list', 'items', '--organizationid', org_id], stdout=subprocess.PIPE)
        
  # convert to json
  items = json.loads(items.stdout.decode("utf-8"))
        
  # write csv headers
  reportWriter.writerow(['Item_Name', 'Item_ID', 'Password_Revision_Date'])
        
  # write items with password revision dates
  for item in items:
    id = (str(item['id']) + ', ')
    name = (str(item['name']) + ', ')
    revisionDate = (str(item['login']['passwordRevisionDate']))
    rowData = ( name + id + revisionDate)
        
reportWriter.writerow
rowData