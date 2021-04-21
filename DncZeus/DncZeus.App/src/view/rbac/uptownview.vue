
<template>
  <div>
    <Card>
      <tables ref="tables"
              editable
              searchable
              height="600" 
              :border="false"
              size="small"
              search-place="top"
              v-model="stores.Uptown.data"
              :totalCount="stores.Uptown.query.totalCount"
              :columns="stores.Uptown.columns"
              @on-delete=""
              @on-edit=""
              @on-select="handleSelect"
              @on-selection-change="handleSelectionChange"
              @on-refresh="handleRefresh"
              :row-class-name="rowClsRender"
              @on-page-change="handlePageChanged"
              @on-page-size-change="handlePageSizeChanged">
        <div slot="search">
          <section class="dnc-toolbar-wrap">
            <Row :gutter="16">
              <Col span="4">
              <Form inline @submit.native.prevent>
                <FormItem>
                  <Input type="text"
                         search
                         :clearable="true"
                         v-model="stores.Uptown.query.kw"
                         placeholder="输入关键字搜索..."
                         @on-search="handleSearchRole()">

                  </Input>
                </FormItem>
              </Form>
              </Col>
              <Col span="1">
              <Button type="primary"
                      icon="md-create"
                      @click="exportData"
                      title="导出">
                导出
              </Button>
              </Col>
              <Col span="18" class="dnc-toolbar-btns">
              <ButtonGroup class="mr3">

                <Button icon="md-refresh" title="刷新" @click="handleRefresh"></Button>
              </ButtonGroup>

              </Col>
            </Row>
          </section>
        </div>
      </tables>
    </Card>

  </div>
</template>


<script>
import Tables from "_c/tables";
import {
    getUptownViewSelect,
    getUptownViewList
  } from "@/api/rbac/Uptown";
  export default {
    name: "	rbac_uptownview_page",
    components: {
      Tables
    },
    data() {
      return {
        stores: {
          Uptown: {
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
            sources: {
              isDeletedSources: [
                { value: -1, text: "全部" },
                { value: 0, text: "正常" },
                { value: 1, text: "已删" }
              ],
              statusSources: [
                { value: -1, text: "全部" },
                { value: 0, text: "禁用" },
                { value: 1, text: "正常" }
              ],
              statusFormSources: [
                { value: 0, text: "禁用" },
                { value: 1, text: "正常" }
              ]
            },
            columns: [
              { type: "selection", width: 50, key: "handle" },
              { title: "数据库名称", key: "dataName", width: 250, sortable: true },
              { title: "客户名称", key: "saleRoomName", width: 200, sortable: true },
              { title: "小区名称", key: "uptownname", width: 200, sortable: true },
              { title: "设备数量", key: "meterCount", width: 100, sortable: true },
              { title: "成功数量", key: "cbwc2count", width: 100, sortable: true },
              { title: "失败数量", key: "cbwc5number", width: 100, sortable: true },
              { title: "离线数量", key: "cbwc4count", width: 100, sortable: true },
              { title: "未知数量", key: "cbwc0count", width: 100, sortable: true },
              { title: "七天抄表率", key: "sevenDp", width: 100, sortable: true },
              
              { title: "一月抄表率", key: "oneMp", width: 100, sortable: true },
              { title: "两月抄表率", key: "twoMp", width: 100, sortable: true },
              { title: "三月抄表率", key: "threeMp", width: 100, sortable: true },
              { title: "今天", key: "toDs", width: 100, sortable: true },
              { title: "一天前", key: "oneDs", width: 80, sortable: true },
              { title: "两天前", key: "twoDs", width: 80, sortable: true },
              { title: "三天前", key: "threeDs", width: 80, sortable: true },
              { title: "四天前", key: "fourDs", width: 80, sortable: true },
              { title: "五天前", key: "fiveDs", width: 80, sortable: true },
              { title: "六天前", key: "sixDs", width: 80, sortable: true },
              { title: "七天前", key: "sevenDs", width: 80, sortable: true },
              { title: "一周前", key: "oneWs", width: 80, sortable: true },
              { title: "两周前", key: "twoWs", width: 80, sortable: true },
              { title: "三周前", key: "threeWs", width: 80, sortable: true },
              { title: "一月前", key: "oneMs", width: 80, sortable: true },
              { title: "两月前", key: "twoMs", width: 80, sortable: true },
              { title: "三月前", key: "threeMs", width: 80, sortable: true },
              
              { title: "正在通讯", key: "cbwc1count", width: 100, sortable: true },
              { title: "占线", key: "cbwc3count", width: 80, sortable: true },
              
              {
                title: "操作",
                align: "center",
                key: "handle",
                width: 150,
                className: "table-command-column",
                options: ["edit"],
                button: [
                  (h, params, vm) => {
                    return h(
                      "Poptip",
                      {
                        props: {
                          confirm: true,
                          title: "你确定要删除吗?"
                        },
                        on: {
                          "on-ok": () => {
                            vm.$emit("on-delete", params);
                          }
                        }
                      },
                      [
                        h(
                          "Tooltip",
                          {
                            props: {
                              placement: "left",
                              transfer: true,
                              delay: 1000
                            }
                          },
                          [
                            h("Button", {
                              props: {
                                shape: "circle",
                                size: "small",
                                icon: "md-trash",
                                type: "error"
                              }
                            }),
                            h(
                              "p",
                              {
                                slot: "content",
                                style: {
                                  whiteSpace: "normal"
                                }
                              },
                              "删除"
                            )
                          ]
                        )
                      ]
                    );
                  },
                  (h, params, vm) => {
                    return h(
                      "Tooltip",
                      {
                        props: {
                          placement: "left",
                          transfer: true,
                          delay: 1000
                        }
                      },
                      [
                        h("Button", {
                          props: {
                            shape: "circle",
                            size: "small",
                            icon: "md-create",
                            type: "primary"
                          },
                          on: {
                            click: () => {
                              vm.$emit("on-edit", params);
                              vm.$emit("input", params.tableData);
                            }
                          }
                        }),
                        h(
                          "p",
                          {
                            slot: "content",
                            style: {
                              whiteSpace: "normal"
                            }
                          },
                          "编辑"
                        )
                      ]
                    );
                  }
                ]
              }
            ],
            data: []
          }
        },
        styles: {
          height: "calc(100% - 55px)",
          overflow: "auto",
          paddingBottom: "53px",
          position: "static"
        }
      }
    },
    methods: {
      UptownViewSelect() {
        getUptownViewSelect(this.stores.Uptown.query).then(res => {
          this.stores.Uptown.data = res.data.data;
          this.stores.Uptown.query.totalCount = res.data.totalCount;
        });
      },
      exportData() {    
        this.$refs.tables.exportCsv({
          filename: "楼幢统计",
          original: false,
          columns: this.stores.Uptown.columns,
          data: this.stores.Uptown.data
        });
      },
      UptownViewList() {
        getUptownViewList(this.stores.Uptown.query).then(res => {
          this.stores.Uptown.data = res.data.data;
          this.stores.Uptown.query.totalCount = res.data.totalCount;
        });
      },
      handleRefresh() {
        this.UptownViewSelect();
      },
      handleSearchRole() {
        this.UptownViewSelect();
      },
      rowClsRender(row, index) {
        if (row.isDeleted) {
          return "table-row-disabled";
        }
        return "";
      },
      handlePageChanged(page) {
        this.stores.Uptown.query.currentPage = page;
        this.UptownViewList();
      },
      handlePageSizeChanged(pageSize) {
        this.stores.Uptown.query.pageSize = pageSize;
        this.UptownViewList();
      }
    },
    mounted() {
      this.UptownViewList();
    }
  };

</script>
