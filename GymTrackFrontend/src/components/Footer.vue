<template>
    <div>
        <b-container class="footer" fluid>
            <b-row>
                <b-col md="3" offset-md="3">
                    <ul>
                        <li> <b-icon icon="terminal-fill"></b-icon><a href="https://github.com/arkadiilviv">GitHub</a></li>
                        <li> <b-icon icon="camera"></b-icon><a href="https://www.instagram.com/iamyourmasterbitch/">Instagram</a></li>
                        <li> <b-icon icon="chat-fill"></b-icon><a href="https://twitter.com/wonder_dat_ass">Twitter</a></li>
                    </ul>
                </b-col>
                <b-col md="3">
                    <ul>
                        <li> <b-icon icon="envelope-fill"></b-icon>Mail: <a href="mailto:lviv.home31@gmail.com">lviv.home31@gmail.com</a></li>
                        <li> <b-icon icon="cursor-fill"></b-icon><a href="https://t.me/invalidnametaken">Telegram</a></li>
                        
                    </ul>
                </b-col>
            </b-row>
            <p class="text-center">All rights reserved (с) {{year}}</p>

        </b-container>
    </div>
</template>
<script>
    export default {
        data: function () {
            return {
                year: 0
            }
        },
        mounted:function(){
            this.year = new Date().getFullYear();
        }
    }
</script>
<style scoped>
    .footer {
        margin-top: 20px;
        background: #005A32;
        color: #fff;
        padding-top: 15px; 
        padding-bottom: 15px; 
    }

    .footer>*:last-child {
        margin-bottom: 0px;
        color: #fff;
    }
    ul{
        list-style: none;
        font-size: 20px;
    }
    a{
        color: white;
    }
    a:hover{
        color: #abf45d;
    }
</style>