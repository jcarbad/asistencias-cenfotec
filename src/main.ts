import { createApp } from 'vue'
import App from './App.vue'
import router from './router'
import { createPinia } from 'pinia'
import { createVfm } from 'vue-final-modal'

import "bootstrap/dist/css/bootstrap.min.css"
import "bootstrap"
import 'vue-final-modal/style.css'

const app = createApp(App)
const vfm = createVfm()
const pinia = createPinia()

app.use(router)
app.use(pinia)
app.use(vfm)
app.mount('#app')
