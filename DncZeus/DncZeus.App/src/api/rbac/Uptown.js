import axios from '@/libs/api.request'

export const getUptownViewSelect = (data) => {
  return axios.request({
    url: 'rbac/UptownView/select',
    method: 'post',
    data
  })
}
export const getUptownViewList = (data) => {
  return axios.request({
    url: 'rbac/UptownView/list',
    method: 'post',
    data
  })
}

export const getUptownSum = (data) => {
  return axios.request({
    url: 'rbac/UptownView/aftersum',
    method: 'get',
    data
  })
}


