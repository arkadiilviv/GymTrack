<template>
  <b-card title="Exercise chart">
    <b-container>
      <b-row>
        <b-col cols="12">
          <b-form-select v-model="selectedExercise" @change="selectExercise" >
            <template v-slot:first>
              <b-form-select-option selected value="null">Please select exercise
              </b-form-select-option>
            </template>
             <b-form-select-option v-for="exercise in exercises" v-bind:key="exercise.id" selected :value="exercise.id">{{exercise.exerciseName}}
                                    </b-form-select-option>
          </b-form-select>
        </b-col>
        <b-col cols="12">
          <ve-line :data="chartData"></ve-line>
        </b-col>
      </b-row>
    </b-container>

  </b-card>
</template>
<script>
  import VeLine from 'v-charts/lib/line.common'


  export default {
    props: ['exerciseData', 'exercises'],
    components: {
      VeLine
    },
    data() {
      return {
        exerciseList: this.exercises,
        chartExerciseData: [],
        selectedExercise: null,
        chartData: {
          columns: ["Date", "Weight"],
          rows: this.chartExerciseData
        },

      }
    },
    methods:{
      selectExercise(){
          var exercise = this.selectedExercise;
          if(exercise != null){
            var result = this.exerciseData.filter(x => x.Exercise == exercise).map(x => {return{'Date':x.Date,'Weight': x.Weight}});
            this.chartExerciseData = result;
            this.chartData.rows = this.chartExerciseData;
          }
      }
    }
  }
</script>