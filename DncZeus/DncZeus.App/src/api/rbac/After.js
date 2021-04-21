import axios from '@/libs/api.request'

export const getAfterViewSelect = (data) => {
  return axios.request({
    url: 'rbac/AfterView/select',
    method: 'post',
    data
  })
}
export const getAfterViewList = (data) => {
  return axios.request({
    url: 'rbac/AfterView/list',
    method: 'post',
    data
  })
}

export const getAfterSum = (data) => {
  return axios.request({
    url: 'rbac/AfterView/aftersum',
    method: 'get',
    data
  })
}


