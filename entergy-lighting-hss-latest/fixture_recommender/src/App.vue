<template src="./templates/App.html"></template>

<script>
export default {
    name: 'fixtureRecommender',
    data () {
        return {
            history: [],
            step: 'start',
            animateBack: false,
            steps_left: -1,
            fixtureFilter: {
                commercial_residential: null,
                purpose: null,
                standard_decorative: null,
                area_feature: null,
                type_of_coverage: null,
                type_of_style: null,
                type_of_light: null,
                illumination_distance: null,
            },
            areaWidth: 3,
            coverage: 50,
            distance: 50,
            style: 1,
            type: 'amber_glow',
            FIXTURES: require('./data/fixtures.json'),
            STEP_TREE: [
                // [<state>, <next step>, <options for step>, <remaining steps>]
                // state = [ res/comm, purpose, standard/deco, area/feature, type of coverage, architectural style, type of light, distance ]
                [[null, null, null, null, null, null, null, null], 'commercial_residential', null, 5],
                    // Commercial
                    [['commercial', null, null, null, null, null, null, null], 'purpose', null, 4],
                        // Commercial -> Work
                        [['commercial', 'work', null, null, null, null, null, null], 'illumination_distance', 'b', 2],
                            // Commercial -> Work -> Up to 75
                            [['commercial', 'work', null, null, null, null, null, 'up_to_75'], 'type_of_light', null, 1],
                                [['commercial', 'work', null, null, null, null, 'amber_glow', 'up_to_75'], 'result', 'amber_glow', 0],
                                [['commercial', 'work', null, null, null, null, 'bright_white', 'up_to_75'], 'result', 'bright_white', 0],
                                [['commercial', 'work', null, null, null, null, 'led', 'up_to_75'], 'result', 'led', 0],
                            // Commercial -> Work -> 76 to 100
                            [['commercial', 'work', null, null, null, null, null, '76_to_100'], 'type_of_light', null, 1],
                                [['commercial', 'work', null, null, null, null, 'amber_glow', '76_to_100'], 'result', 'amber_glow', 0],
                                [['commercial', 'work', null, null, null, null, 'bright_white', '76_to_100'], 'result', 'bright_white', 0],
                                [['commercial', 'work', null, null, null, null, 'led', '76_to_100'], 'result', 'led', 0],
                            // Commercial -> Work -> 101 to 130
                            [['commercial', 'work', null, null, null, null, null, '101_to_130'], 'type_of_light', null, 1],
                                [['commercial', 'work', null, null, null, null, 'amber_glow', '101_to_130'], 'result', 'amber_glow', 0],
                                [['commercial', 'work', null, null, null, null, 'bright_white', '101_to_130'], 'result', 'bright_white', 0],
                                [['commercial', 'work', null, null, null, null, 'led', '101_to_130'], 'result', 'led', 0],
                        // Commercial -> Parking
                        [['commercial', 'parking', null, null, null, null, null, null], 'area_feature', null, 3],
                            // Commercial -> Parking -> Feature (limits to Acorn, shows light options)
                            [['commercial', 'parking', null, 'feature', null, null, null, null], 'type_of_light', null, 1],
                                [['commercial', 'parking', null, 'feature', null, null, 'amber_glow', null], 'result', 'amber_glow', 0],
                                [['commercial', 'parking', null, 'feature', null, null, 'bright_white', null], 'result', 'bright_white', 0],
                                [['commercial', 'parking', null, 'feature', null, null, 'led', null], 'result', 'led', 0],
                            // Commercial -> Parking -> Area
                            [['commercial', 'parking', null, 'area', null, null, null, null], 'standard_decorative', null, 2],
                                // Commercial -> Parking -> Area -> Standard
                                [['commercial', 'parking', 'standard', 'area', null, null, null, null], 'type_of_light', null, 1],
                                    [['commercial', 'parking', 'standard', 'area', null, null, 'amber_glow', null], 'result', 'amber_glow', 0],
                                    [['commercial', 'parking', 'standard', 'area', null, null, 'bright_white', null], 'result', 'bright_white', 0],
                                    [['commercial', 'parking', 'standard', 'area', null, null, 'led', null], 'result', 'led', 0],
                                // Commercial -> Parking -> Area -> Decorative
                                [['commercial', 'parking', 'decorative', 'area', null, null, null, null], 'type_of_light', null, 1],   
                                    [['commercial', 'parking', 'decorative', 'area', null, null, 'amber_glow', null], 'result', 'amber_glow', 0],
                                    [['commercial', 'parking', 'decorative', 'area', null, null, 'bright_white', null], 'result', 'bright_white', 0],
                                    [['commercial', 'parking', 'decorative', 'area', null, null, 'led', null], 'result', 'led', 0],
                        // Commercial -> Pedestrian
                        [['commercial', 'pedestrian', null, null, null, null, null, null, null], 'illumination_distance', 'a', 2],
                            // Commercial -> Pedestrian -> Short
                            [[ 'commercial', 'pedestrian', null, null, null, null, null, 'short'], 'type_of_light', null, 1],
                                [['commercial', 'pedestrian', null, null, null, null, 'amber_glow', 'short'], 'result', 'amber_glow', 0],
                                [['commercial', 'pedestrian', null, null, null, null, 'led', 'short'], 'result', 'led', 0],
                                [['commercial', 'pedestrian', null, null, null, null, 'bright_white', 'short'], 'result', 'bright_white', 0],
                            // Commercial -> Pedestrian -> Wide
                            [['commercial', 'pedestrian', null, null, null, null, null, 'wide'], 'type_of_light', null, 1],
                                [['commercial', 'pedestrian', null, null, null, null, 'amber_glow', 'wide'], 'result', 'amber_glow', 0],
                                [['commercial', 'pedestrian', null, null, null, null, 'led', 'wide'], 'result', 'led', 0],
                                [['commercial', 'pedestrian', null, null, null, null, 'bright_white', 'wide'], 'result', 'bright_white', 0],
                    // Residential
                    [['residential', null, null, null, null, null, null, null], 'type_of_coverage', 'a', 2],
                        // Residential -> Closed Space
                        [['residential', null, null, null, 'close_space', null, null, null], 'type_of_light', null, 1],
                            [['residential', null, null, null, 'close_space', null, 'amber_glow', null], 'result', 'amber_glow', 0],
                            [['residential', null, null, null, 'close_space', null, 'bright_white', null], 'result','bright_white', 0],
                            [['residential', null, null, null, 'close_space', null, 'led', null], 'result', 'led', 0],
                        // Residential -> Open Area
                        [['residential', null, null, null, 'open_area', null, null, null], 'type_of_light', null, 1],
                            [['residential', null, null, null, 'open_area', null, 'amber_glow', null], 'result', 'amber_glow', 0],
                            [['residential', null, null, null, 'open_area', null, 'bright_white', null], 'result', 'bright_white', 0],
                            [['residential', null, null, null, 'open_area', null, 'led', null], 'result', 'led', 0],
            ],
        };
    },
    computed: {
        matching () {
            var self = this;
            var filtered = this.FIXTURES;
            for(var filter in this.fixtureFilter) {
                if(this.fixtureFilter[filter] !== null) {
                    filtered = filtered.filter(function(fixture) {
                        return fixture[filter].indexOf(self.fixtureFilter[filter]) !== -1;
                    });
                }
            }
            return filtered;
        },
        matchingCount () {
            return this.matching.length;
        },
        relativeImagePath () {       
          // quick workaround ... currently assumes we're never showing the recommender on homepage.
          if (window.location.href.indexOf("fixture/") > -1) {
            return "../../";
          } else {
            return "../";
          }
        },
        relativePath () {       
          // quick workaround ... currently assumes we're never showing the recommender on homepage.
          if (window.location.href.indexOf("fixture/") > -1) {
            return "../";
          } else {
            return "./";
          }
        },
        
        progress () {
            return this.steps_left === -1
                ? '0%'
                : (100 / 6 * (6 - this.steps_left)) + "%";
        },
        showHeader () {
            return this.step != 'start' && this.step != 'commercial_residential';
        },
        coveragePoints () {
            return "90,50 100,280 " + (100 + (Math.max(30, this.coverage) / 100) * 490) + ",280";
        },
        distancePoints () {
            return "90,50 100,280 " + (100 + (Math.max(30, this.distance) / 100) * 490) + ",280";
        },
        state () {
            return [
                this.fixtureFilter['commercial_residential'],
                this.fixtureFilter['purpose'],
                this.fixtureFilter['standard_decorative'],
                this.fixtureFilter['area_feature'],
                this.fixtureFilter['type_of_coverage'],
                this.fixtureFilter['type_of_style'],
                this.fixtureFilter['type_of_light'],
                this.fixtureFilter['illumination_distance'],
            ];
        },
        isIE () {
            var ua = window.navigator.userAgent;
            if (ua.indexOf('MSIE ') > 0) return true;
            if (ua.indexOf('Trident/') > 0)  return true;
            if (ua.indexOf('Edge/') > 0) return true;
            return false;
        },
    },
    methods: {
        setStep () {
            var state = this.state;

            console.log(state);

            for (var i = 0; i < this.STEP_TREE.length; i++) {
                var match = true;
                for (var j = 0; j < state.length; j++) {
                    if(!(state[j] == this.STEP_TREE[i][0][j] || this.STEP_TREE[i][0][j] == "*"))
                        match = false;
                };
                
                if(match) {
                    this.steps_left = this.STEP_TREE[i][3];
                    this.options = this.STEP_TREE[i][2];
                    this.step = this.STEP_TREE[i][1];
                    break;
                }
            };

            this.dataLayerPush({
                'event': 'efr-step',
                'efr-step': this.step,
                'efr-stepsLeft': this.steps_left,
                'efr-state': this.state
            });

            if(this.step == 'result') {
                this.dataLayerPush({
                    'event': 'efr-result',
                    'efr-resultsCount': this.matchingCount,
                    'efr-results': this.matching.map(e => e.name),
                    'efr-state': this.state
                });
            }
        },
        begin () {
            this.dataLayerPush({'event': 'efr-begin'});
            this.animateBack = false;
            this.setStep();
        },
        back () {
            this.animateBack = true;
            var c = this.history.pop();
            switch(c) {
                case 'commercial_residential': this.fixtureFilter['commercial_residential'] = null; break;
                case 'purpose': this.fixtureFilter['purpose'] = null; break;
                case 'standard_decorative': this.fixtureFilter['standard_decorative'] = null; break;
                case 'area_feature': this.fixtureFilter['area_feature'] = null; break;
                case 'type_of_coverage': this.fixtureFilter['type_of_coverage'] = null; break;
                case 'type_of_style': this.fixtureFilter['type_of_style'] = null; break;
                case 'type_of_light': this.fixtureFilter['type_of_light'] = null; break;
                case 'illumination_distance': this.fixtureFilter['illumination_distance'] = null; break;
            }

            this.dataLayerPush({
                'event': 'efr-back',
                'efr-step': this.step,
                'efr-state': this.state
            });

            this.setStep();
        },
        select (selection) {
            switch(selection) {
                case 'commercial': this.fixtureFilter['commercial_residential'] = 'commercial'; break;
                case 'residential': this.fixtureFilter['commercial_residential'] = 'residential'; break;

                case 'security': this.fixtureFilter['purpose'] = 'security'; break;
                case 'work': this.fixtureFilter['purpose'] = 'work'; break;
                case 'parking': this.fixtureFilter['purpose'] = 'parking'; break;
                case 'pedestrian': this.fixtureFilter['purpose'] = 'pedestrian'; break;

                case 'standard': this.fixtureFilter['standard_decorative'] = 'standard'; break;
                case 'decorative': this.fixtureFilter['standard_decorative'] = 'decorative'; break;

                case 'area':
                    this.fixtureFilter['area_feature'] = this.areaWidth > 3
                        ? 'area'
                        : 'feature';
                    break;

                case 'coverage':
                    this.fixtureFilter['type_of_coverage'] = this.coverage < 50
                        ? 'close_space'
                        : 'open_area';
                    break;

                case 'style':
                    switch(this.style) {
                        case 2: this.fixtureFilter['type_of_style'] = 'modern'; break;
                        case 1: this.fixtureFilter['type_of_style'] = 'transitional'; break;
                        case 0: this.fixtureFilter['type_of_style'] = 'traditional'; break;
                    }
                    break;

                case 'type': this.fixtureFilter['type_of_light'] = this.type; break;

                case 'distance':
                    if (this.options == "b") {
                        if (this.distance < 41) { 
                            this.fixtureFilter['illumination_distance'] = "up_to_75";
                        } else if (this.distance < 75) { 
                            this.fixtureFilter['illumination_distance'] = "76_to_100";
                        } else {
                            this.fixtureFilter['illumination_distance'] = "101_to_130";
                        }
                    } else {
                        this.fixtureFilter['illumination_distance'] = this.distance < 53 ? 'short' : 'wide';
                    }
                    break;
            }

            this.dataLayerPush({
                'event': 'efr-select',
                'efr-step': this.step,
                'efr-selectedOption': this.fixtureFilter[this.step],
                'efr-state': this.state
            });

            this.animateBack = false;
            this.history.push(this.step);
            this.setStep();
        },
        entergyContactUs (fixture) {
            this.dataLayerPush({
                'event': 'efr-contact',
                'efr-result': fixture.name,
                'efr-state': this.state
            });
            if(typeof window.entergyContactUs === "function") {
                window.entergyContactUs([fixture.name, this.state]);
            }
        },
        viewDetails (fixture) {
            this.dataLayerPush({
                'event': 'efr-details',
                'efr-result': fixture.name,
                'efr-state': this.state
            });
        },
        dataLayerPush (event) {
            if(typeof dataLayer == "object" && typeof dataLayer.push == "function") {
                dataLayer.push(event);
            } else {
                console.warn("dataLayer not available", event);
            }
        },
    }
}
</script>

<style lang="scss" scoped src="./styles/App.scss"></style>
