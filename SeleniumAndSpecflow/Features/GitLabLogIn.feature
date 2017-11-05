Feature: GitLabLogIn
	In order to begin using gitLab
	I try to log in the system 
	from login page

Background: 
	Given I navigate to https://gitlab.com/users/sign_in

Scenario: Log in with valid credentials 
	When I fill log in form with valid credentials
	| Username			  | Password |
	| kurganovk@gmail.com |29069547a |
	And I press login button
	Then I should be thrown to main page

Scenario: Log in with invalid credentials 
	When I fill log in form with invalid credentials
	| Username			 | Password |
	| kurganov@gmail.com | 29069547  |
	And I press login button 
	Then I should see red alert box

Scenario: Log in with no credentials put in
	When I leave login form blank
	And I press login button
	Then I should see warning message under blank fields
	
Scenario: I want to stay logged in after closing browser
	When I fill log in form with valid credentials
	| Username			  | Password |
	| kurganovk@gmail.com |29069547a |
	And I clicked Remember me check box
	And I press login button
	Then I should be thrown to main page
	And After reopening browser I stay logged in 

 Scenario: I dont want to stay logged in after closing browser
	When I fill log in form with valid credentials
	| Username			  | Password |
	| kurganovk@gmail.com |29069547a |
	And I press login button
	Then I should be thrown to main page
	And After reopening browser I see about page  