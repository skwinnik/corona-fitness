<template>
    <div>
        <div class="test-area">
            <div class="element" v-for="element in elements"
                 :key="element"
                 :id="element"
                 :style="styles[element]"
            >
                <div class="element-inner">
                    <div>
                        <div>
                            {{element}}
                        </div>
                        <div>
                            width: {{styles[element].width}}
                        </div>
                        <div>
                            height: {{styles[element].height}}
                        </div>
                    </div>
                </div>
            </div>
        </div>

        <div class="controls-area">
            <button @click="count++">
                Increase
            </button>
            <button @click="count--">
                Decrease
            </button>
        </div>
    </div>
</template>

<script>
    import {LayoutCalculatorPercentage} from '../scripts/layoutCalculator.js';

    export default {
        data: function () {
            return {
                count: 10,
            };
        },

        computed: {
            elements: function () {
                return [...Array(this.count).keys()].map(x => 'element' + x)
            },
            styles: function () {
                console.log('styles calc...');
                return new LayoutCalculatorPercentage(this.elements).getStyles()
            }
        }
    }
</script>

<style lang="scss">
    .test-area {
        position: relative;
        width: 600px;
        height: 600px;
    }

    .element {
        transition: all .4s;
        overflow: hidden;
        
        &-inner {
            display: flex;
            align-items: center;
            justify-content: center;
            height: 100%;
        }
    }
</style>