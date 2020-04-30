<template>
    <div>
        <b-card title="Add Exercise">
            <div>
                <b-container>
                    <b-row>
                        <b-col cols="12">
                            <b-form-select v-model="selectedExercise"  >
                                <template v-slot:first>
                                    <b-form-select-option selected value="null">Please select exercise
                                    </b-form-select-option>
                                </template>
                                
                                 <b-form-select-option v-for="exercise in exercises" v-bind:key="exercise.id" selected :value="exercise.id">{{exercise.exerciseName}}
                                    </b-form-select-option>

                               
                            </b-form-select>
                        </b-col>
                    </b-row>
                    <b-row class="mt-3">
                        <b-col cols="3">
                            <label :for="weight">Weight:</label>
                        </b-col>
                        <b-col cols="9">
                            <b-form-input v-model="weight" placeholder="Weight" type="number"></b-form-input>
                        </b-col>

                    </b-row>
                    <b-row class="mt-3">
                        <b-col cols="3"><label :for="date">Date:</label></b-col>
                        <b-col cols="6">
                            <date-pick v-model="date" :displayFormat="'DD.MM.YYYY'"></date-pick>
                        </b-col>
                        <b-col cols="3">
                             <b-button class="float-right" @click="addExercise" variant="success">
                        <b-icon icon="plus"></b-icon>
                    </b-button>
                        </b-col>
                    </b-row>
                </b-container>

            </div>
        </b-card>
    </div>

</template>
<script>
    import DatePick from 'vue-date-pick';
    import 'vue-date-pick/dist/vueDatePick.css';
     
    export default {
        props: ['exercises'],
        components: {
            DatePick
        },
        data: () => ({
            date: "2020.01.01",
            selectedExercise: null,
            weight: 0
        }),
        methods:{
            // Toast(message,title,variant = "default")
            //     {
            //     this.$bvToast.toast(message, {
            //         variant: variant,
            //         title: title,
            //         autoHideDelay: 5000
            //         })
            //     },
            addExercise(){
                if(this.selectedExercise == null)
                    this.$Toast('Select exercise!','Warning','warning');
                else if(this.weight < 1)
                    this.$Toast('Select weight!','Warning','warning');
                else if(this.date === "")
                    this.$Toast('Select date!','Warning','warning');
                else{
                    var item = {
                        Date:this.date,
                        Exercise:this.selectedExercise,
                        Weight: Number.parseFloat(this.weight)
                     
                        };
                  
                    
                    this.$emit('add-exercise', item);
                    this.selectedExercise = null;
                    this.weight = 0;
                    this.$Toast('Added!','Success','success');
                }
                     

            }
        },
        mounted: function(){
            document.getElementsByTagName('input')[2].classList.add('form-control');
        }
    };
</script>