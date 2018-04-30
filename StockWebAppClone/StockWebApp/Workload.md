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

```seq
User->App: item info
Note right of App: input is standardized
App-->Db: does this item\n already exist?
Note left of Db: Nope
Db->>User: Good to go!
Note left of Db: Sure does
Db-->App: This item exists\n current stock:
Note left of App: Current item info
App->User: Would you like\n to add to this item?
User->App: Corrected info
App-->Db: info persisted
User->>User: returned to\n staging screen
```