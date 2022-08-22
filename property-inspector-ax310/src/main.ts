import { createApp } from 'vue'
import { createPinia } from 'pinia'
import './style.css'
import App from './App.vue'
import { useCreatorCentralStore } from './stores/creatorCentral'

const pinia = createPinia()
const app = createApp(App)
app.use(pinia)
app.mount('#app')

declare global {
    interface Window {
        connectCreatorCentral: any;
        state: any;
    }
}

const store = useCreatorCentralStore()
window.state = store;

window.connectCreatorCentral =
    (inPort: number, inPropertyViewUuid: string, inRegisterEvent: string, inInfo: any, inWidgetInfo: any) => store.connectToCreatorCentral(inPort, inPropertyViewUuid, inRegisterEvent, inInfo, inWidgetInfo)