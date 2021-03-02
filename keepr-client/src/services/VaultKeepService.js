import { api } from './AxiosService'

const baseURL = '/api/vaultkeeps/'

class VaultKeepService {
  async create(newVK) {
    await api.post(baseURL, newVK)
  }

  async delete(id) {
    await api.delete(baseURL + id)
  }
}

export const vaultKeepService = new VaultKeepService()
