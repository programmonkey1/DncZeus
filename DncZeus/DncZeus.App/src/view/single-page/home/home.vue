<template>
  <div>
    <Row :gutter="10">
      <i-col :xs="12" :md="8" :lg="4" v-for="(infor, i) in stores.StatisticsReport.columns" :key="`infor-${i}`" style="height: 120px;padding-bottom: 10px;">
        <infor-card shadow :color="infor.color" :icon="infor.icon" :icon-size="36">
        <count-to :end="infor.key" count-class="count-style" />
        <p>{{ infor.title }}</p>

        </infor-card>
        </i-col>
      </Row>
      <Row>
        <Card shadow>
          <example style="height: 310px;" />
        </Card>
      </Row>
      <Row :gutter="10" style="margin-top: 10px;">
        <i-col :md="24" :lg="8" style="margin-bottom: 20px;">
          <Card shadow>
            <chart-pie style="height: 300px;" :value="stores.StatisticsReport.pieData" text="饼图"></chart-pie>
          </Card>
        </i-col>
        <i-col :md="24" :lg="16" style="margin-bottom: 20px;">
          <Card shadow>
            <chart-bar style="height: 300px;" :value="barData" text="抄表率" />
          </Card>
        </i-col>
      </Row>

    </div>

  </template>


  <script>
    import InforCard from '_c/info-card'
    import CountTo from '_c/count-to'
    import { ChartPie, ChartBar } from '_c/charts'
    import Example from './example.vue'
    import { getStatisticsReportSelect } from "@/api/rbac/statisticsreport";
    export default {
      name: 'home',
      components: {
        InforCard,
        CountTo,
        ChartPie,
        ChartBar,
        Example
      },

      data() {
        return {
          center: [116.397428, 39.90923],
          zoom: 15,
          label: {
            content: '自定义标题',
            offset: [10, 12]
          },
          page: {
            currPage: 1,
            list: [],
            pageSize: 100,
            totalCount: 1,
            totalPage: 5,
          },
          formModel: {
            opened: false,
            title: "创建角色",
            mode: "create",
            selection: [],
              fields: {
                numberofcustomers:2,
                numberofdistricts: 3,
                quantityofwatermeters: 1,
                numberofsuccesses: 0,
                numberoffailures: 0,
                offlinequantity: 0,
                numberofnewfiles: 0,
                timeofday: '2021-03-18',
              }
          },
          stores: {
            StatisticsReport: {
              query: {
                totalCount: 0,
                pageSize: 20,
                currentPage: 1,
                kw: "",
                isDeleted: 0,
                status: -1,
                sort: [
                  {
                    direct: "DESC",
                    field: "CreatedOn"
                  }
                ]
              },
              columns: [

                { title: '客户数量', icon: 'md-person-add', key:1, color: '#2d8cf0' },
                { title: '楼栋数量', icon: 'md-locate', key:0, color: '#19be6b' },
                { title: '水表数量', icon: 'md-help-circle', key: 0, color: '#19be6b' },
                { title: '成功数量', icon: 'md-help-circle', key:0, color: '#19be6b' },
                { title: '失败数量', icon: 'md-share', key:0, color: '#ed3f14' },

                { title: '离线数量', icon: 'md-map', key: 0, color: '#9A66E4' },
                { title: '新建档案数量', icon: 'md-chatbubbles', key:0, color: '#E46CBB' },
                { title: '抄表截止日10天', icon: 'md-bonfire', key: 0, color: '#ed3f14' },
                { title: '抄表截止日5天', icon: 'ios-bonfire-outline', key: 0, color: '#ed3f14' },
                { title: '延期抄表截止', icon: 'ios-bonfire', key: 'abc', color: '#ed3f14' }


              ],
              data: [],
              pieData: [
                { value: '100', name: '成功' }
              ]
            }

          },


          barData: {
            Mon: 10,
            Tue: 20,
            Wed: 30,
            Thu: 0,
            Fri: 0,
            Sat: 0,
            Sun: 0
          }
        }
      },
      mounted() {
        this.loadRoleList();
      },
      methods: {
        loadRoleList() {
          getStatisticsReportSelect().then(res => {
            this.stores.StatisticsReport.data = res.data.data;
            this.stores.StatisticsReport.columns[0].key = res.data.data[0].numberofcustomers;
            this.stores.StatisticsReport.columns[1].key = res.data.data[0].numberofdistricts;
            this.stores.StatisticsReport.columns[2].key = res.data.data[0].quantityofwatermeters;
            this.stores.StatisticsReport.columns[3].key = res.data.data[0].numberofsuccesses;
            this.stores.StatisticsReport.columns[4].key = res.data.data[0].numberoffailures;
            this.stores.StatisticsReport.columns[5].key = res.data.data[0].offlinequantity;
            this.stores.StatisticsReport.columns[6].key = res.data.data[0].numberofnewfiles;
            this.stores.StatisticsReport.columns[7].key = res.data.data[0].timeofday;
            console.log(this.stores.StatisticsReport.pieData[0].value)
            this.stores.StatisticsReport.pieData[0].value = res.data.data[0].numberofsuccesses;
            console.log(this.stores.StatisticsReport.pieData[0].value)
          //  this.stores.StatisticsReport.pieData[0].name = "成功数量1";
          //  this.stores.StatisticsReport.pieData[1].value = res.data.data[0].numberoffailures;
         //   this.stores.StatisticsReport.pieData[2].value = res.data.data[0].offlinequantity;

            this.stores.StatisticsReport.query.totalCount = res.data.totalCount;
          });
        }

      }

    }
  </script>

  <style lang="less">
    .count-style {
      font-size: 35px;
    }

    #container {
      width: 300px;
      height: 180px;
    }
  </style>
