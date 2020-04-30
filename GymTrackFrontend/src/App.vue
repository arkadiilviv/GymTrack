<template>
  <div id="app">
    <Navbar ref="Navbar" @login-callback="loginCallback"/>
    <b-container class="mt-5">
      <b-row>
        <b-col cols="12">
          <div>
            <b-jumbotron header="GymTracker" lead="Quick and easy" class="gradientFirst">
              <p>Manage your workout profit!</p>
            </b-jumbotron>
          </div>
        </b-col>
      </b-row>
      <b-row v-if="token">
        <b-col cols="12">
          <h2>Exercise Management <b-icon icon="book"> </b-icon>
          </h2>
          <hr>
        </b-col>
        <b-col col cols="12" sm="12" md="6" lg="6">
          <CreateExercise @create-exercise="createExercise" />
        </b-col>
        <b-col col cols="12" sm="12" md="6" lg="6">
          <AddExercise v-bind:exercises="exercises" @add-exercise="addExercise" />
        </b-col>
        <b-col cols="12" class="mt-3">
          <b-alert show variant="info"><b>Note:</b> you need to create and add some exercises to see Statistics.
          </b-alert>
        </b-col>
        <b-col cols="12">
          <h2>Statistics <b-icon icon="bar-chart"> </b-icon>
          </h2>
          <hr>
        </b-col>
        <b-col cols="12">
          <Chart :exerciseData="exerciseData" :exercises="exercises" ref="exerciseChart" />
        </b-col>
      </b-row>
      <b-row v-else>
        <b-col>
          <b-jumbotron header="Sign in" lead="Please log in your account">
            <p>To get access for functionality</p>
            <b-button variant="primary" v-on:click="showAuth">Sign in</b-button>
          </b-jumbotron>
        </b-col>
      </b-row>
      <b-row>
        <b-col cols="12" class="mt-5">
          <div>
            <b-jumbotron header="GymCharts" lead="Summary of all your work" class="gradientSecond">
              <p>See you progress in charts below</p>
            </b-jumbotron>
          </div>
        </b-col>
      </b-row>
      <b-row v-if="token">
        <b-col col cols="12" sm="12" md="6" lg="6">
          <ExerciseChart :exerciseData="weightData" />
        </b-col>
        <b-col col cols="12" sm="12" md="6" lg="6">
          <DateChart :exerciseData="dateWeightData" />
        </b-col>
      </b-row>
    </b-container>
    <Footer />
  </div>
</template>
<script>
  import Navbar from './components/Navbar.vue'
  import CreateExercise from './components/CreateExercise.vue'
  import AddExercise from './components/AddExercise.vue'
  import Chart from './components/Chart.vue'
  import ExerciseChart from './components/ExercisesChart.vue'
  import DateChart from './components/DateChart.vue'
  import Footer from './components/Footer.vue'
  import axios from 'axios'
 

  export default {

    name: 'app',
    data() {
      return {
        todos: ['Vue', 'React', 'Angular'],
        exercises: [],
        exerciseData: [],
        weightData: [],
        dateWeightData: [],
        token: "",
        username: "",
        host:"http://localhost:52841/api/values"
      }
    }

    ,
    components: {
      Navbar,
      CreateExercise,
      AddExercise,
      Chart,
      ExerciseChart,
      DateChart,
      Footer
    }
    ,mounted: function(){
       // eslint-disable-next-line no-console
        console.log(localStorage.getItem("username"));
        var username =  this.$cookie.get("username");
        var token =  this.$cookie.get("token");
        if(token!= null)
           {
             this.token = token;
             this.username = username;
             this.loadExercises();
           }
    }
    ,
    methods: {
      showAuth:function(){
        this.$refs.Navbar.showAuth();
      },
      loginCallback(){
          var username =  this.$cookie.get("username");
          var token =  this.$cookie.get("token");
           // eslint-disable-next-line no-console
           console.log('Username '+username+', token: '+token+'');
           
             this.token = token;
             this.username = username;
             this.loadExercises();
           
      },
      deleteTodo(index) {
        this.todos.splice(index, 1);
      }

      ,
      createExercise(name) {
       
         axios({
            method:'put',
            url:this.host,
            params: {'exerciseName': name},
            headers:{  
              'Authorization': 'Bearer ' + this.token
            }
          }).then( response => 
            {
                // eslint-disable-next-line no-console
                    console.log(response.data);
                    this.loadExercises();
                    //response.data.forEach(element =>  this.exercises.push(element));
                   
            }
          )
      }

      ,
      addExercise(item, axiosSend = true) {

        var duplicate = this.exerciseData.findIndex(x => x.Date === item.Date && x.Exercise === item.Exercise);
        if (duplicate > -1) this.exerciseData[duplicate].Weight += item.Weight;
        else this.exerciseData.push(item);

        this.addWeightItem(item);
        this.addDateWeightItem(item);
        if(axiosSend){
                axios({
                    method: 'post',
                    url: this.host,
                    headers:{  
                    'Authorization': 'Bearer ' + this.token
                    },
                    data:item
                }).then(response => {
                    // eslint-disable-next-line no-console
                    console.log(response.data);
                });
        }
        this.$refs.exerciseChart.selectExercise();

      }

      ,
      addWeightItem(item) {
        var tempItem = {
          'Exercise': this.exercises.find(x => x.id == item.Exercise).exerciseName,
          'Weight': item.Weight
        }

        ;
        var duplicate = this.weightData.findIndex(x => x.Exercise === tempItem.Exercise);
        if (duplicate > -1) this.weightData[duplicate].Weight += tempItem.Weight;
        else this.weightData.push(tempItem);
      }

      ,
      addDateWeightItem(item) {
        var tempItem = {
          'Date': item.Date,
          'Weight': item.Weight
        }

        ;
        var duplicate = this.dateWeightData.findIndex(x => x.Date == tempItem.Date);
        if (duplicate > -1) this.dateWeightData[duplicate].Weight += tempItem.Weight;
        else this.dateWeightData.push(tempItem);
      },
      loadExercises(){
        this.exercises = [];
        axios.defaults.headers.common['Authorization'] = this.token;
          axios({
            method:'get',
            url:this.host,
            headers:{  
              'Authorization': 'Bearer ' + this.token
            }
          }).then( response => 
            {
                // eslint-disable-next-line no-console
                    console.log(response.data);
                    response.data.forEach(element =>  this.exercises.push(element));
                    this.loadExerciseData();
                   
            }
          )
      },
      loadExerciseData(){
        this.exerciseData = [];
        this.exercises.forEach(element => this.loadExerciseDataAjax(element.id));
      },
      loadExerciseDataAjax(id){
        axios.defaults.headers.common['Authorization'] = this.token;
          axios({
            method:'get',
            url:this.host,
            params: {'id': id},
            headers:{  
              'Authorization': 'Bearer ' + this.token
            }
          }).then( response => 
            {
                // eslint-disable-next-line no-console
                    console.log(response.data);
                    response.data.forEach(element => {
                        var result = {Exercise: element.id, Date: element.date, Weight:element.weight };
                        this.addExercise(result,false);
                    });
                   
                   
            }
          )

      }
    }
  }
</script>
<style>
  .bg-success {
    background-color: #00B076 !important;
  }

  .btn-success {
    background-color: #00B076 !important;
  }

  .gradientFirst {

    background: #4ec4d7;
    /* Old browsers */
    background: -moz-linear-gradient(-45deg, #4ec4d7 1%, #4ec4d7 61%, #4ec4d7 61%, #006e96 100%);
    /* FF3.6-15 */
    background: -webkit-linear-gradient(-45deg, #4ec4d7 1%, #4ec4d7 61%, #4ec4d7 61%, #006e96 100%);
    /* Chrome10-25,Safari5.1-6 */
    background: linear-gradient(135deg, #4ec4d7 1%, #4ec4d7 61%, #4ec4d7 61%, #006e96 100%);
    /* W3C, IE10+, FF16+, Chrome26+, Opera12+, Safari7+ */
    filter: progid:DXImageTransform.Microsoft.gradient(startColorstr='#4ec4d7', endColorstr='#006e96', GradientType=1);
    /* IE6-9 fallback on horizontal gradient */
  }

  .gradientSecond {
    color: #EAEAEA !important;
    background: #006e96;
    /* Old browsers */
    background: -moz-linear-gradient(-45deg, #006e96 1%, #4ec4d7 100%);
    /* FF3.6-15 */
    background: -webkit-linear-gradient(-45deg, #006e96 1%, #4ec4d7 100%);
    /* Chrome10-25,Safari5.1-6 */
    background: linear-gradient(135deg, #006e96 1%, #4ec4d7 100%);
    /* W3C, IE10+, FF16+, Chrome26+, Opera12+, Safari7+ */
    filter: progid:DXImageTransform.Microsoft.gradient(startColorstr='#006e96', endColorstr='#4ec4d7', GradientType=1);
    /* IE6-9 fallback on horizontal gradient */
  }
</style>