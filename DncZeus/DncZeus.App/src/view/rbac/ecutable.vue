<template>
  <div>
    <Card>
      <tables ref="tables"
              editable
              searchable
              :border="false"
              size="small"
              search-place="top"
              v-model="stores.ecutable.data"
              :totalCount="stores.ecutable.query.totalCount"
              :columns="stores.ecutable.columns"
              @on-delete="handleDelete"
              @on-edit="handleEdit"
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
                         v-model="stores.ecutable.query.kw"
                         placeholder="输入关键字搜索..."
                         @on-search="handleSearchRole()">
                  <Select slot="prepend"
                          v-model="stores.ecutable.query.isDeleted"
                          @on-change="handleSearchRole"
                          placeholder="删除状态"
                          style="width:60px;">
                    <Option v-for="item in stores.ecutable.sources.isDeletedSources"
                            :value="item.value"
                            :key="item.value">
                      {{item.text}}
                    </Option>
                  </Select>
                  <Select slot="prepend"
                          v-model="stores.ecutable.query.status"
                          @on-change="handleSearchRole"
                          placeholder="角色状态"
                          style="width:60px;">
                    <Option v-for="item in stores.ecutable.sources.statusSources"
                            :value="item.value"
                            :key="item.value">
                      {{item.text}}
                    </Option>
                  </Select>
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
              <Col span="1" >
              <Upload ref="upload"
                      action="/api/book/excel/import"
                      name="excel-file"
                      :show-upload-list="true"
                      :on-format-error="handleFormatError"
                      :on-success="handleSuccess"
                      :on-error="handleError"
                      :format="['xlsx','xls']">
                <Button type="primary" icon="ios-cloud-upload-outline">批量导入</Button>
              </Upload>
              </Col>
              <Col span="18" class="dnc-toolbar-btns">

              <ButtonGroup class="mr3">

                <Button class="txt-danger"
                        icon="md-trash"
                        title="删除"
                        @click="handleBatchCommand('delete')"></Button>
                <Button class="txt-success"
                        icon="md-redo"
                        title="恢复"
                        @click="handleBatchCommand('recover')"></Button>
                <Button class="txt-danger"
                        icon="md-hand"
                        title="禁用"
                        @click="handleBatchCommand('forbidden')"></Button>
                <Button class="txt-success"
                        icon="md-checkmark"
                        title="启用"
                        @click="handleBatchCommand('normal')"></Button>
                <Button icon="md-refresh" title="刷新" @click="handleRefresh"></Button>
              </ButtonGroup>
              <Button icon="md-create"
                      type="primary"
                      @click="handleShowCreateWindow"
                      title="新增角色">
                新增角色
              </Button>
              </Col>
            </Row>
          </section>
        </div>
      </tables>
    </Card>
    <Drawer :title="formTitle"
            v-model="formModel.opened"
            width="400"
            :mask-closable="true"
            :mask="true"
            :styles="styles">
     
      <Form :model="formModel.fields" ref="formRole" :rules="formModel.rules" label-position="left">
        <Row :gutter="32">
          <Col span="12">
          <FormItem label="设备编号" prop="electronicUnitNumber" label-position="left">
            <Input v-model="formModel.fields.electronicUnitNumber" placeholder="请输入设备编号" />
          </FormItem>
          </Col>
          <Col span="12">
          <FormItem label="产品型号" prop="productModel" label-position="left">
            <Input v-model="formModel.fields.productModel" placeholder="请输入产品型号" />
          </FormItem>
          </Col>
        </Row>
        <Row :gutter="32">
          <Col span="12">
          <FormItem label="生产日期" prop="dateOfManufacture" label-position="left">
            <Input v-model="formModel.fields.dateOfManufacture" placeholder="请输入生产日期" />
          </FormItem>
          </Col>
          <Col span="12">
          <FormItem label="程序版本" prop="programVersion" label-position="left">
            <Input v-model="formModel.fields.programVersion" placeholder="请输入程序版本" />
          </FormItem>
          </Col>
        </Row>
        <Row :gutter="32">
          <Col span="12">
          <FormItem label="通讯方式" prop="communicationMode" label-position="left">
            <Input v-model="formModel.fields.communicationMode" placeholder="请输入通讯方式" />
          </FormItem>
          </Col>
          <Col span="12">
          <FormItem label="平均电流" prop="averageWorkingCurrent" label-position="left">
            <Input v-model="formModel.fields.averageWorkingCurrent" placeholder="请输入平均电流" />
          </FormItem>
          </Col>
        </Row>
        <Row :gutter="32">
          <Col span="12">
          <FormItem label="防护等级" prop="protectionLevel" label-position="left">
            <Input v-model="formModel.fields.protectionLevel" placeholder="请输入防护等级" />
          </FormItem>
          </Col>
          <Col span="12">
          <FormItem label="信号强度" prop="signalIntensity" label-position="left">
            <Input v-model="formModel.fields.signalIntensity" placeholder="请输入信号强度" />
          </FormItem>
          </Col>
        </Row>
        <Row :gutter="32">
          <Col span="12">
          <FormItem label="IMSI" prop="imsi" label-position="left">
            <Input v-model="formModel.fields.imsi" placeholder="请输入IMSI号" />
          </FormItem>
          </Col>
          <Col span="12">
          <FormItem label="磁干扰状态" prop="magneticInterferenceStatus" label-position="left">
            <Input v-model="formModel.fields.magneticInterferenceStatus" placeholder="请输入磁干扰状态" />
          </FormItem>
          </Col>
        </Row>
        <Row :gutter="32">
          <Col span="12">
          <FormItem label="IMEI" prop="imei" label-position="left">
            <Input v-model="formModel.fields.imei" placeholder="请输入IMEI号" />
          </FormItem>
          </Col>
          <Col span="12">
          <FormItem label="电磁型号" prop="batteryModel" label-position="left">
            <Input v-model="formModel.fields.batteryModel" placeholder="请输入电磁型号" />
          </FormItem>
          </Col>
        </Row>
        <Row :gutter="32">
          <Col span="12">
          <FormItem label="电压状态" prop="voltageState" label-position="left">
            <Input v-model="formModel.fields.voltageState" placeholder="请输入电压状态" />
          </FormItem>
          </Col>
          <Col span="12">
          <FormItem label="电压" prop="voltage" label-position="left">
            <Input v-model="formModel.fields.voltage" placeholder="请输入电压" />
          </FormItem>
          </Col>
        </Row>

        <FormItem label="设备状态" label-position="left">
          <i-switch size="large" v-model="formModel.fields.status" :true-value="1" :false-value="0">
            <span slot="open">正常</span>
            <span slot="close">禁用</span>
          </i-switch>
        </FormItem>
        <FormItem label="备注" label-position="top">
          <Input type="textarea"
                 v-model="formModel.fields.remarks"
                 :rows="4"
                 placeholder="备注信息" />
        </FormItem>
      </Form>
        <div class="demo-drawer-footer">
          <Button icon="md-checkmark-circle" type="primary" @click="handleSubmitRole">保 存</Button>
          <Button style="margin-left: 8px" icon="md-close" @click="formModel.opened = false">取 消</Button>
        </div>
    </Drawer>
  </div>
</template>

<script>import Tables from "_c/tables";
import {
  getRoleList,
  createRole,
  loadRole,
  editRole,
  deleteRole,
  batchCommand
} from "@/api/rbac/ECUTable";
export default {
  name: "rbac_ecutable_page",
  components: {
    Tables
  },
  data() {
    return {
      commands: {
        delete: { name: "delete", title: "删除" },
        recover: { name: "recover", title: "恢复" },
        forbidden: { name: "forbidden", title: "禁用" },
        normal: { name: "normal", title: "启用" }
      },
      formModel: {
        opened: false,
        title: "创建角色",
        mode: "create",
        selection: [],
        fields: {
          ecuid: "",
          electronicUnitNumber: "212",
          productModel: "LXSY-",        
          programVersion: "V.0",
          communicationMode: "NB- IoT",
          averageWorkingCurrent: "20u",
          protectionLevel: "IP68",
          imsi: "1111111111111111",
          imei: "2222222222222222",
          signalIntensity: "20",
          magneticInterferenceStatus: "无",
          voltageState: "正常",
          voltage: "3.6V",
          batteryModel: "E34615",
          dateOfManufacture:"2021-03-11",
          remarks: "",
         
          status: 1,
          isDeleted: 0
        },
        rules: {
          name: [
            {
              type: "string",
              required: true,
              message: "请输入角色名称",
              min: 2
            }
          ]
        }
      },
      stores: {
        ecutable: {
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
           
            { title: "设备编号", key: "electronicUnitNumber", width: 100, sortable: true },
            {
              title: "状态",
              key: "status",
              align: "center",
              width: 80,
              render: (h, params) => {
                let status = params.row.status;
                let statusColor = "success";
                let statusText = "正常";
                switch (status) {
                  case 0:
                    statusText = "禁用";
                    statusColor = "default";
                    break;
                }
                return h(
                  "Tooltip",
                  {
                    props: {
                      placement: "top",
                      transfer: true,
                      delay: 500
                    }
                  },
                  [
                    //这个中括号表示是Tooltip标签的子标签
                    h(
                      "Tag",
                      {
                        props: {
                          //type: "dot",
                          color: statusColor
                        }
                      },
                      statusText
                    ), //表格列显示文字
                    h(
                      "p",
                      {
                        slot: "content",
                        style: {
                          whiteSpace: "normal"
                        }
                      },
                      statusText //整个的信息即气泡内文字
                    )
                  ]
                );
              }
            },
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
            },
            { title: "产品型号", key: "productModel", width: 100, sortable: true },
            { title: "生产日期", key: "dateOfManufacture", width: 150, sortable: true, ellipsis: true, tooltip: true },
            { title: "程序版本", key: "programVersion", width: 100, sortable: true, ellipsis: true, tooltip: true },
            { title: "通讯方式", key: "communicationMode", width: 100, sortable: true, ellipsis: true, tooltip: true},
            { title: "平均电流", key: "averageWorkingCurrent", width: 100, sortable: true, ellipsis: true, tooltip: true},
            { title: "防护等级", key: "protectionLevel", width: 100, sortable: true, ellipsis: true, tooltip: true },        
            { title: "IMSI", key: "imsi", width: 100, sortable: true, ellipsis: true, tooltip: true},
            { title: "IMEI", key: "imei", width: 100, sortable: true, ellipsis: true, tooltip: true },            
            { title: "信号强度", key: "signalIntensity", width: 100, sortable: true, ellipsis: true, tooltip: true },
            { title: "IP", key: "magneticInterferenceStatus", width: 100, sortable: true, ellipsis: true, tooltip: true},
            { title: "电压状态", key: "voltageState", width: 100, sortable: true, ellipsis: true, tooltip: true},
            { title: "电压", key: "voltage", width: 100, sortable: true, ellipsis: true, tooltip: true},
            { title: "电池型号", key: "batteryModel", width: 100, sortable: true, ellipsis: true, tooltip: true },
            { title: "备注", key: "remarks", width: 200, sortable: true, ellipsis: true, tooltip: true }
            
            
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
    };
  },
  computed: {
    formTitle() {
      if (this.formModel.mode === "create") {
        return "创建角色";
      }
      if (this.formModel.mode === "edit") {
        return "编辑角色";
      }
      return "";
    },
    selectedRows() {
      return this.formModel.selection;
    },
    selectedRowsId() {
      return this.formModel.selection.map(x => x.ecuid);
    }
  },
  methods: {
    loadRoleList() {
      getRoleList(this.stores.ecutable.query).then(res => {
        this.stores.ecutable.data = res.data.data;
        this.stores.ecutable.query.totalCount = res.data.totalCount;
      });
    },
    exportData() {
      this.$refs.tables.exportCsv({
        filename: "仪表信息",
        original: false,
        columns: this.stores.ecutable.columns,
        data: this.stores.ecutable.data
      });
    },
    handleFormatError(file) {
      this.$Notice.warning({
        title: '文件格式不正确',
        desc: '文件 ' + file.name + ' 格式不正确，请上传.xls,.xlsx文件。'
      })
    },
    handleSuccess(res, file) {
      if (res.errcode === 0) {
        this.dialoLead = false
        this.$Message.success("数据导入成功！")
        this._getBookList()
        this.$refs.upload.clearFiles()
      }
    },
    handleError(error, file) {
      this.$Message.error("数据导入失败！")
    },

    handleOpenFormWindow() {
      this.formModel.opened = true;
    },
    handleCloseFormWindow() {
      this.formModel.opened = false;
    },
    handleSwitchFormModeToCreate() {
      this.formModel.mode = "create";
    },
    handleSwitchFormModeToEdit() {
      this.formModel.mode = "edit";
      this.handleOpenFormWindow();
    },
    handleEdit(params) {
      this.handleSwitchFormModeToEdit();
      this.handleResetFormRole();
      this.doLoadRole(params.row.ecuid);
    },
    handleSelect(selection, row) {},
    handleSelectionChange(selection) {
      this.formModel.selection = selection;
    },
    handleRefresh() {
      this.loadRoleList();
    },
    handleShowCreateWindow() {
      this.handleSwitchFormModeToCreate();
      this.handleOpenFormWindow();
      this.handleResetFormRole();
    },
    handleSubmitRole() {
      let valid = this.validateRoleForm();
      if (valid) {
        if (this.formModel.mode === "create") {
          this.doCreateRole();
        }
        if (this.formModel.mode === "edit") {
          this.doEditRole();
        }
      }
    },
    handleResetFormRole() {
      this.$refs["formRole"].resetFields();
    },
    doCreateRole() {
      createRole(this.formModel.fields).then(res => {
        if (res.data.code === 200) {
          this.$Message.success(res.data.message);
          this.loadRoleList();
        } else {
          this.$Message.warning(res.data.message);
        }
        this.handleCloseFormWindow();
      });
    },
    doEditRole() {
      editRole(this.formModel.fields).then(res => {
        if (res.data.code === 200) {
          this.$Message.success(res.data.message);
          this.loadRoleList();
        } else {
          this.$Message.warning(res.data.message);
        }
        this.handleCloseFormWindow();
      });
    },
    validateRoleForm() {
      let _valid = false;
      this.$refs["formRole"].validate(valid => {
        if (!valid) {
          this.$Message.error("请完善表单信息");
          _valid = false;
        } else {
          _valid = true;
        }
      });
      return _valid;
    },
    doLoadRole(ecuid) {
      loadRole({ ecuid: ecuid }).then(res => {
        this.formModel.fields = res.data.data;
      });
    },
    handleDelete(params) {
      this.doDelete(params.row.ecuid);
    },
    doDelete(ids) {
      if (!ids) {
        this.$Message.warning("请选择至少一条数据");
        return;
      }
      deleteRole(ids).then(res => {
        if (res.data.code === 200) {
          this.$Message.success(res.data.message);
          this.loadRoleList();
        } else {
          this.$Message.warning(res.data.message);
        }
      });
    },
    handleBatchCommand(command) {
      if (!this.selectedRowsId || this.selectedRowsId.length <= 0) {
        this.$Message.warning("请选择至少一条数据");
        return;
      }
      this.$Modal.confirm({
        title: "操作提示",
        content:
          "<p>确定要执行当前 [" +
          this.commands[command].title +
          "] 操作吗?</p>",
        loading: true,
        onOk: () => {
          this.doBatchCommand(command);
        }
      });
    },
    doBatchCommand(command) {
      batchCommand({
        command: command,
        ids: this.selectedRowsId.join(",")
      }).then(res => {
        if (res.data.code === 200) {
          this.$Message.success(res.data.message);
          this.loadRoleList();
          this.formModel.selection=[];
        } else {
          this.$Message.warning(res.data.message);
        }
        this.$Modal.remove();
      });
    },
    handleSearchRole() {
      this.loadRoleList();
    },
    rowClsRender(row, index) {
      if (row.isDeleted) {
        return "table-row-disabled";
      }
      return "";
    },
    handlePageChanged(page) {
      this.stores.ecutable.query.currentPage = page;
      this.loadRoleList();
    },
    handlePageSizeChanged(pageSize) {
      this.stores.ecutable.query.pageSize = pageSize;
      this.loadRoleList();
    }
  },
  mounted() {
    this.loadRoleList();
  }
};</script>
