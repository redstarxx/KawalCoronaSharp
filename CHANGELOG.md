# Version 1.2.0
- BREAKING: Change `GetGlobalDataAsync` & `GetPartialResponseDataAsync` method name to `GetAllCountriesDataAsync` & `GetPartialGlobalDataAsync`.
- Include `SearchMode` enum in the `GetCountryDataAsync` method to fetch the exact or closest matching country name data.
- Excess zeros from unix timestamp are now fully trimmed.

# Version 1.1.1
- Changed `LocalResponseEntity.Healed` property name to `Recovered`.
- Fix XML documentation not showing.

# Version 1.1.0
- Added GetPartialResponseDataAsync to fetch global statistics separately (positives, recovered, deaths).

# Version 1.0.0
- Basic implementation of the API.