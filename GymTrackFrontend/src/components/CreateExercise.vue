<template>
    <div>
        <b-card title="Create exercise">
            <b-container>
            <b-row>
                <b-col cols="2"><label :for="exerciseName">Name:</label></b-col>
                <b-col cols="8">
                    <b-form-input v-model="exerciseName" placeholder="Exercise name"></b-form-input>
                </b-col>
                <b-col cols="2">
                    <b-button  class="float-right" @click="createExercise(exerciseName)" variant="success">
                        <b-icon icon="plus"></b-icon>
                    </b-button>
                </b-col>

            </b-row>
            </b-container>
        </b-card>
    </div>
</template>
<script>
    export default {
        name: 'CreateExercise',
        data() {
            return {
                exerciseName: ''
            }
        },
        methods: {
            createExercise(name) {
                if (name !== ''){
                    this.$Toast('Exercises created!','Success','success');
                    this.$emit('create-exercise', name);
                    this.exerciseName = "";

                }
            }
        }
    }
</script>