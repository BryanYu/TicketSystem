import 'bootstrap/dist/css/bootstrap.css'
import '@fortawesome/fontawesome-free/css/all.min.css'
import 'startbootstrap-sb-admin-2/css/sb-admin-2.min.css'
import 'jquery/src/jquery.js'
import 'bootstrap/dist/js/bootstrap.bundle.js'
import 'jquery.easing/jquery.easing.min.js'
import 'startbootstrap-sb-admin-2/js/sb-admin-2.js'
import './assets/css/google-fonts-nunito.css'
import { createApp } from 'vue'
import App from './App.vue'
import router from './router'


const app = createApp(App)
app.use(router).mount('#app')