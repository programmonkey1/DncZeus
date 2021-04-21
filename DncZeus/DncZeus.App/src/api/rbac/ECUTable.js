import axios from '@/libs/api.request'

export const getRoleList = (data) => {
  return axios.request({
    url: 'rbac/ecutable/list',
    method: 'post',
    data
  })
}

// createRole
export const createRole = (data) => {
  return axios.request({
    url: 'rbac/ecutable/create',
    method: 'post',
    data
  })
}

//loadRole
export const loadRole = (data) => {
  return axios.request({
    url: 'rbac/ecutable/edit/' + data.ecuid,
    method: 'get'
  })
}

// editRole
export const editRole = (data) => {
  return axios.request({
    url: 'rbac/ecutable/edit',
    method: 'post',
    data
  })
}

// delete role
export const deleteRole = (ids) => {
  return axios.request({
    url: 'rbac/ecutable/delete/' + ids,
    method: 'get'
  })
}

// batch command
export const batchCommand = (data) => {
  return axios.request({
    url: 'rbac/ecutable/batch',
    method: 'get',
    params: data
  })
}


// loadEcutableDataSource

// find Ecutable data source by keyword
export const findEcutableDataSourceByKeyword = (data) => {
  return axios.request({
    url: 'rbac/ecutable/find_list_by_kw/' + data.keyword,
    method: 'get'
  })
}

