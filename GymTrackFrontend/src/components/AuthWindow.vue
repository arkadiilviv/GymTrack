<template>
    <div>
        <b-nav-item v-b-modal.my-modal v-if="token === null">Sign in<b-icon icon="box-arrow-right" ></b-icon>
        </b-nav-item>
        <b-nav-item v-else v-on:click="signOff()">Hello, {{usernameTitle}} sign off <b-icon icon="box-arrow-left" ></b-icon></b-nav-item>
        <b-modal hide-footer id="my-modal" title="Sign in" ref="authModal">
            <b-tabs pills align="center">
                
                <b-tab title="Log in" active>
                    <form class="login" @submit.prevent="login">
                        <label>User name</label>
                        <b-input required v-model="username" type="text" placeholder="Snoopy" />
                        <label>Password</label>
                        <b-input required v-model="password" type="password" placeholder="Password" />
                        <hr />
                        <b-button type="submit" variant="success">Login</b-button>
                    </form>
                </b-tab>
                <b-tab title="Register">
                    <form class="register" @submit.prevent="register">
                        <label>User name</label>
                        <b-input required v-model="username" type="text" placeholder="Snoopy" />
                        <label>Password</label>
                        <b-input required v-model="password" type="password" placeholder="Password" />
                        <hr />
                        <b-button type="submit" variant="success">Register</b-button>
                    </form>
                </b-tab>
            </b-tabs>
        </b-modal>
    </div>
</template>
<script>
import axios from 'axios'
    export default {
        data: function () {
            return {
                usernameTitle: "name",
                username: "",
                password: "",
                token: "",
                host: "http://localhost:52841/api/account"
                
                
            }
        },
        mounted: function(){
            this.usernameTitle = this.$cookie.get("username");
            this.token = this.$cookie.get("token");
        },
        methods: {
            showWindow() {
                this.$refs['authModal'].show();
            },
            login(){
                 axios({
                    method: 'post',
                    url: this.host,
                    data:{
                        'username': this.username,
                        'password': this.password}
                }).then(response => {
                    // eslint-disable-next-line no-console
                    console.log(response.data);
                    this.$cookie.set("token",response.data.access_token,{expires: (response.data.time.toString()+'m')});
                    this.$cookie.set("username",response.data.username,{expires: (response.data.time.toString()+'m')});
                    this.usernameTitle = response.data.username;
                    this.token = response.data.access_token;
                    this.$bvModal.hide('my-modal');
                    this.clearFields();
                    this.$emit('login-callback');
                    
                }).catch(error => {
                    this.$Toast(error.response.data,"Error","danger");
                    
                    
                });
            },
            register(){
                axios({
                    method: 'put',
                    url: this.host,
                    data:{
                        'username': this.username,
                        'password': this.password}
                }).then(response => {
                    // eslint-disable-next-line no-console
                    console.log(response.data);
                   
                    
                    this.clearFields();
                    this.$Toast("User created!","Success","success");
                    
                }).catch(error => {
                    this.$Toast(error.response.data,"Error","danger");
                    
                    
                });
            },
            signOff(){
                
                this.token = null;
                this.usernameTitle = "";
                this.$cookie.delete("username");
                this.$cookie.delete("token");
                this.clearFields();
                this.$emit('login-callback');
            },
            clearFields(){
                this.username = "";
                this.password = "";
            }
        }
    }
</script>
<style scoped>
    a {
        color: rgba(255, 255, 255, 1) !important;
    }
</style>