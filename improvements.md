

# Future Improvements
# 1. Improve Data Mocking for Tests
Instead of reading data directly from `ITollDataProvider` in the tests, consider mocking the data or finding a more flexible way to inject the necessary data. This will make the tests more isolated, easier to configure, and potentially reduce coupling between components.

## 2. Configurable Toll-Free Months
This means we could use a configuration file or service to set which months are toll-free, making it easier to adapt to changes in policy. * and now july its just hardcoded in test and code 

## 3. API and Endpoint Versioning
All applications grow, we’ll want to introduce versioning for our API endpoints. This will help us manage changes smoothly and ensure that older versions remain functional, allowing users to transition at their own pace.

## 4. Improved Charge Calculation Logic
We May need consider refactoring our charge calculation to be year-specific. This way, we can adjust pricing strategies based on the current year and its associated holidays. By injecting this configuration, we can keep our logic clean and adaptable.

## 5. Create a Schema for API request and response
Defining a clear schema for our API responses would help standardize the data format. 

## 6. CI/CD Pipeline for Testing
To maintain high code quality, we should set up a Continuous Integration/Continuous Deployment (CI/CD) pipeline.

# 7. Improve Data Mocking for Tests
Instead of reading data directly from `ITollDataProvider` in the tests, consider mocking the data or finding a more flexible way to inject the necessary data. This will make the tests more isolated, easier to configure, and potentially reduce coupling between components.

# 8. Test Improvements
- **Expanding Test Coverage**: Ensuring that all possible scenarios are tested, including edge cases.
- **Parameterized Tests**: Using parameterized tests to reduce code duplication and improve test coverage for various inputs.
- **Mocking Dependencies**: Utilizing mocking frameworks to isolate tests from external dependencies, allowing for more reliable and faster tests.
- **Integration Testing**: Implementing integration tests to validate the complete workflow and interactions between different components of the system.
- **Error Handling Tests**: Creating tests to simulate error scenarios, ensuring that our application handles exceptions gracefully.
