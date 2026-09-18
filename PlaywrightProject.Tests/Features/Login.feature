Feature: User Authentication

  Scenario: Generate token with valid credentials
    Given I have valid user credentials
    When I send a POST request to generate token
    Then the response status code should be 200
    And the response should contain a valid token