import axios from '@/libs/api.request'

export const getStatisticsReportSelect = (data) => {
  return axios.request({
    url: 'rbac/StatisticsReport/select',
    method: 'get',
    data
  })
}
export const getStatisticsReportList = (data) => {
  return axios.request({
    url: 'rbac/StatisticsReport/list',
    method: 'post',
    data
  })
}

export const getStatisticsReportIndex = (data) => {
  return axios.request({
    url: 'rbac/StatisticsReport/Index',
    method: 'get',
    data
  })
}
