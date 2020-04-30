<template>
     <b-card title="Exercises Weight">
    <b-container>
      <b-row>
        <b-col cols="12">
          <ve-pie :data="chartData"></ve-pie>
        </b-col>
      </b-row>
    </b-container>

  </b-card>
</template>
<script>
import VePie from 'v-charts/lib/pie.common.js'
export default {
    props:['exerciseData'],
     components: {
      VePie
    },
    data() {
      return {
        chartData: {
          columns: ["Exercise", "Weight"],
          rows: this.exerciseData
        },

      }
    }
}
</script>