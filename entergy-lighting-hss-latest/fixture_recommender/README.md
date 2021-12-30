# entergy_fixture_recommender

> Entergy Fixture Recommender

## Build Setup

``` bash
# install dependencies
npm install

# serve with hot reload at localhost:8080
npm run dev

# build for production with minification. Builds into a single js file - copy from fixture_recommender/dist/fixtureRecommender.js` to `/security-lighting-microsite/scripts/fixtureRecommender.js to bring into the project.
npm run build
```

For detailed explanation on how things work, consult the [docs for vue-loader](http://vuejs.github.io/vue-loader).

## dataLayer Events

* `efr-begin`: Begin-Button on first slide is pressed.
* `efr-step`: Step is displayed.
   * Parameter `efr-step`
   * Parameter `efr-stepsLeft`: Number between 0 and 6
   * Parameter `efr-state`: Current State
* `efr-select`: User selects an option on the current step.
    * Parameter `efr-step`
    * Parameter `efr-selectedOption`
    * Parameter `efr-state`: Current State
* `efr-back`: User went back one step
    * Parameter `efr-step`: Current step
    * Parameter `efr-state`: Current State
* `efr-result`: Results are shown.
    * Parameter `efr-resultsCount`: Number of results (1 or 2)
    * Parameter `efr-results`: Array of the result (names of the fixtures)
    * Parameter `efr-state`: Current State
* `efr-details`: Clicked "View Feature Details"-Button on result page
    * Parameter `efr-result`: Name of the result
    * Parameter `efr-state`: Current State
* `efr-contact`: Clicked "Contact Us"-Button on result page
    * Parameter `efr-result`: Name of the result
    * Parameter `efr-state`: Current State


Possible values for `efr-step` are: `start`, `commercial_residential`, `purpose`, `standard_decorative`, `area_feature`, `type_of_coverage`, `type_of_style`, `type_of_light`, `illumination_distance` or `result`

Possible values for `efr-selectedOption`
|`step`|Options|
|----------|-------|
| `commercial_residential` | `commercial` or `residential` |
| `purpose` | `work`, `parking` or `pedestrian` |
| `standard_decorative` | `standard` or `decorative` |
| `area_feature` | `area` or `feature` |
| `type_of_coverage` | `close_space` or `open_area` |
| `type_of_style` | `modern`, `transitional` or `traditional` |
| `type_of_light` | `amber_glow`, `bright_white` or `led` |
| `illumination_distance` | `up_to_75`, `76_to_100`, `101_to_130`, `short` or `wide` |
