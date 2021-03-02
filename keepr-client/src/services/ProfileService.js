import { AppState } from '../AppState'
import { api } from './AxiosService'
const baseURL = '/api/profiles/'
class ProfileService {
  async getProfile(id) {
    const res = await api.get(baseURL + id)
    AppState.activeProfile = res.data
  }

  async getVaults(id) {
    const res = await api.get(baseURL + id + '/vaults')
    AppState.activeProfileVaults = res.data
  }

  async getKeeps(id) {
    const res = await api.get(baseURL + id + '/keeps')
    AppState.activeProfileKeeps = res.data
  }
}

export const profileService = new ProfileService()
