import axios from '@/libs/api.request'

export const getRoleList = (data) => {
  return axios.request({
    url: 'rbac/deviceInformation/list',
    method: 'post',
    data
  })
}

// createRole
export const createRole = (data) => {
  return axios.request({
    url: 'rbac/deviceInformation/create',
    method: 'post',
    data
  })
}

//loadRole
export const loadRole = (data) => {
  return axios.request({
    url: 'rbac/deviceInformation/edit/' + data.diid,
    method: 'get'
  })
}

// editRole
export const editRole = (data) => {
  return axios.request({
    url: 'rbac/deviceInformation/edit',
    method: 'post',
    data
  })
}

// delete role
export const deleteRole = (ids) => {
  return axios.request({
    url: 'rbac/deviceInformation/delete/' + ids,
    method: 'get'
  })
}

// batch command
export const batchCommand = (data) => {
  return axios.request({
    url: 'rbac/deviceInformation/batch',
    method: 'get',
    params: data
  })
}

// loadEcutableDataSource

// find Ecutable data source by keyword
export const finddeviceInformationDataSourceByKeyword = (data) => {
  return axios.request({
    url: 'rbac/deviceInformation/find_list_by_kw/' + data.keyword,
    method: 'get'
  })
}



