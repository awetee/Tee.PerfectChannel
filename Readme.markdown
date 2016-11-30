1. It took a about 6 to 7 hours to develop
2. To compile and run: 
	* Restore nuget packages
	* update database using entity framework code first (package manager console: update-database)
	* Run tha application
3. Assumptions: 
	* I made the assumption that POST data will be sent in raw JSON(application/json) format
4. Design justification: 
	* I chose to use a generic repository patter as it allows for the most reuse of persistence code. 
	* Also chose to refactor into services to avoid clogging up the controllers and allow for isolated testing.
	* I would also, instead of hard deleting BasketItems, add a flag e.g. isCheckedOut or Ordered to basket items and only retrieve one where the flag is false;
	* Some integration tests would have been nice
	* Given more time, I would extract the update method out of the repository and give the resposibility to the caller for improved performance when inserting multiple items before saving.
	* Also realised i could actually do away with the basket table and simply retrieve the baskketItem using the userId

5. API Endpoints
	
	User Story 1
	ItemController:
	
		[GET]
		http://localhost:52692/api/item/
	
	User Story 2
	BasketController:
	
		[GET]
		http://localhost:52692/api/basket/GetBasket/First User

		[POST]
		http://localhost:52692/api/basket/AddBasketEntry/1 ; body: { "userId": 1, {"ItemId": 1, "Quantity": 2}}

	User Story 3
	BasketController:
	
		[POST]
		http://localhost:52692/api/basket/AddBasketEntries/1 ; body:[{"ItemId": 1, "Quantity": 2}, {"ItemId": 2, "Quantity": 3}]
		
	User Story 4
	BasketController:
	
		[GET]
		http://localhost:52692/api/basket/Checkout/First User
		