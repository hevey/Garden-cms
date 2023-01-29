import {defineStore} from "pinia";

export type GardenState = {
    token: string,
    isAuthenticated: boolean
}
export const useGardenStore = defineStore('garden', {
    state: () => {
        return {
            token: "",
            isAuthenticated: false
        } as GardenState
    },
    persist: true,
    actions: {
        getToken() {
            return this.token
        },
        
        getIsAuthenticated() {
            return this.isAuthenticated
        },
        
        setToken(value: string) {
            this.token = value
        },
        
        setIsAuthenticated(value: boolean) {
            this.isAuthenticated = value
        }
    }
})
