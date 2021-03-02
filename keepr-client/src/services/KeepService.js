import { AppState } from '../AppState'
import { api } from './AxiosService'

const baseURL = '/api/keeps/'

class KeepService {
  async getAll() {
    const res = await api.get(baseURL)
    AppState.homeKeeps = res.data
  }

  async getOne(id) {
    const res = await api.get(baseURL + id)
    AppState.activeKeep = res.data
    const index = AppState.homeKeeps.findIndex(keep => keep.id === res.data.id)
    AppState.homeKeeps[index] = res.data
  }

  async create(newKeep) {
    await api.post(baseURL, newKeep)
  }

  async edit(newKeep) {
    await api.put(baseURL + newKeep.id, newKeep)
  }

  async delete(id) {
    await api.delete(baseURL + id)
  }
}

export const keepService = new KeepService()
