<template>
    <div>
        <b-navbar fixed="top" toggleable="lg" type="dark" variant="success">
            <b-navbar-brand href="#"><b-icon icon="calendar"></b-icon> GymTrack</b-navbar-brand>

            <b-navbar-toggle target="nav-collapse"></b-navbar-toggle>

            <b-collapse id="nav-collapse" is-nav>
                <b-navbar-nav>
                  
                </b-navbar-nav>

                <!-- Right aligned nav items -->
                <b-navbar-nav class="ml-auto">
                    <b-nav-item v-b-modal.info-modal>Info</b-nav-item>
                    <AuthWindow ref="AuthWindow" @login-callback="loginCallback"/>
                </b-navbar-nav>
            </b-collapse>
        </b-navbar>
        <div>

            <b-modal hide-footer id="info-modal" title="GymTrack">
                <h2 class="my-4">Hello!</h2>
                <p>GymTrack is easy to use tool for tracking your progress in gym.</p>
            </b-modal>
        </div>
    </div>
</template>
<script scoped>
    import AuthWindow from './AuthWindow'
    export default {
        name: 'Navbar',
         components: {
             AuthWindow
         },
         methods:{
             showAuth(){
                  this.$refs.AuthWindow.showWindow();
             },
             loginCallback(){
                 this.$emit('login-callback');
             }
         }
    }
</script>