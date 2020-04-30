export default{
    install(Vue){
        Vue.prototype.$Toast = function (message,title,variant = "default"){
            this.$bvToast.toast(message, {
                variant: variant,
                title: title,
                autoHideDelay: 5000
                });
        }
    }
}