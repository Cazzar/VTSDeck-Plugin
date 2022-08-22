import { useEventBus, useWebSocket, WebSocketStatus, UseWebSocketReturn } from "@vueuse/core";
import { defineStore } from "pinia";
import { ref, watch } from "vue";

let wsReturn: UseWebSocketReturn<any>|undefined = undefined;

export const useCreatorCentralStore = defineStore('creator-central', {
    state: () => {
        return {
            onMessage: useEventBus<any>('cc'),
            status: ref<WebSocketStatus>("CLOSED"),
        }
    },
    getters: {
        isConnected: (state) => state.status == "OPEN"
    },
    actions: {
        connectToCreatorCentral(inPort: number, inPropertyViewUuid: string, inRegisterEvent: string, inInfo: any, inWidgetInfo: any): void {
            let state = this;

            wsReturn = useWebSocket(
                //`ws://localhost:${inPort}`,
                `wss://ws.postman-echo.com/raw`,
                {
                    onConnected(ws) {
                        ws.send(JSON.stringify({
                            event: inRegisterEvent,
                            uuid: inPropertyViewUuid,
                        }))
                    },
                    onMessage(ws, event) {
                        state.onMessage.emit('message', event.data)
                    },
                }
            )

            watch(wsReturn.status, status => this.status = status)
        },
        send: (data: any) => wsReturn?.send(JSON.stringify(data)),
    },
})