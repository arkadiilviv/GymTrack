<template>
     <b-card title="Exercises Date">
    <b-container>
      <b-row>
        <b-col cols="12">
          <ve-histogram :data="chartData"></ve-histogram>
        </b-col>
      </b-row>
    </b-container>

  </b-card>
</template>
<script>
import VeHistogram from 'v-charts/lib/histogram.common.js'
export default {
    props:['exerciseData'],
     components: {
      VeHistogram
    },
    data() {
      return {
        chartData: {
          columns: ["Date", "Weight"],
          rows: this.exerciseData
        },

      }
    }
}
</script>