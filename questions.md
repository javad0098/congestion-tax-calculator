# Questions

## General Questions
1. **Vehicle Classification**: 
   - Are motorbikes and motorcycles considered the same vehicle type for our calculations, or should we treat them differently? 
   - If they are different, do we need to implement a converter to handle the variations in their definitions, or can we simply pair their syntax in our vehicle factory?

2. **Testing Criteria**:
   - What should be our baseline for testing? How much testing is considered enough to ensure the reliability of our application?
   - Should we focus more on unit tests, integration tests, or end-to-end tests?

3. **API Structure**:
   - Is the current API endpoint structure intuitive enough for users, or do we need to reconsider the naming conventions?

## Code-Specific Questions
1. **Toll Calculation Logic**:
   - In the `GetTollFee` method, should we refine the logic further to improve clarity, or is it sufficient as it stands?

2. **Error Handling**:
   - Are we adequately handling all potential errors in the API, particularly with respect to invalid vehicle types?

3. **Date Handling**:
   - Are there any edge cases in date handling that we might be overlooking? For example, how should we treat dates that fall on public holidays or during weekends?
   - Should we implement more comprehensive validation for the date formats to avoid runtime exceptions?

## Additional Considerations
- Are there any dependencies in our project that we should consider replacing or upgrading?

