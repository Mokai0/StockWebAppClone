#Steps to working out this duplication problem
#I need a way to check if the item already exists in the storeroom:
- if so to notify the user that it does
- if not continue like normal

###In order to standardise the item information being added 
1. Convert all the incoming information into uppercase
2. Add logic that checks against the Db to see if the item exists
	- this will probably be handled by obtaining a HashCode for the object based on it's:
	BRAND + CATEGORY + PRODUCTNAME
	- furthermore this my be checked against the Product Id in a key/value pair
	https://blogs.msdn.microsoft.com/ericlippert/2011/02/28/guidelines-and-rules-for-gethashcode/


User -> item info -> App
App: standardizes info
App -> standardized info -> Db
Db: checks for existing data
Db = !match ? All clear! : [Data is sent back]
App -> item already exists, add quantity -> User
etc...