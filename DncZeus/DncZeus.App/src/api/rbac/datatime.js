import axios from '@/libs/api.request'

export const getdatatimeList = (data) => {
  return axios.request({
    url: 'rbac/datalist/list',
    method: 'post',
    data
  })
}
