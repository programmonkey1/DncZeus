<style>
    .ivu-table .demo-table-info-row td{
        background-color: #2db7f5;
        color: #fff;
    }
    .ivu-table .demo-table-error-row td{
        background-color: #ff6600;
        color: #fff;
    }
    .ivu-table td.demo-table-info-column{
        background-color: #2db7f5;
        color: #fff;
    }
    .ivu-table .demo-table-info-cell-name {
        background-color: #2db7f5;
        color: #fff;
    }
    .ivu-table .demo-table-info-cell-age {
        background-color: #ff6600;
        color: #fff;
    }
    .ivu-table .demo-table-info-cell-address {
        background-color: #187;
        color: #fff;
    }
</style>
<template>
  <div>
    <Card>
      <tables ref="tables"
              editable
              searchable
              :border="false"
              size="small"
              search-place="top"
              v-model="stores.worktask.data"
              :totalCount="stores.worktask.query.totalCount"
              :columns="stores.worktask.columns"
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
                         v-model="stores.worktask.query.kw"
                         placeholder="输入关键字搜索..."
                         @on-search="handleSearchRole()">
                  <Select slot="prepend"
                          v-model="stores.worktask.query.isDeleted"
                          @on-change="handleSearchRole"
                          placeholder="删除状态"
                          style="width:60px;">
                    <Option v-for="item in stores.worktask.sources.isDeletedSources"
                            :value="item.value"
                            :key="item.value">
                      {{item.text}}
                    </Option>
                  </Select>
                  <Select slot="prepend"
                          v-model="stores.worktask.query.status"
                          @on-change="handleSearchRole"
                          placeholder="角色状态"
                          style="width:60px;">
                    <Option v-for="item in stores.worktask.sources.statusSources"
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
              <Col span="1">
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
                      title="新增主题">
                新增主题
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
          <FormItem label="设备编号" prop="eunumber" label-position="left">
            <Input v-model="formModel.fields.eunumber" placeholder="请输入设备编号" />
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
          <FormItem label="电子单元编号"  prop="ecuid" label-position="left">
            <Input v-model="formModel.fields.ecuid" disabled placeholder="请输入参数规格" />
          </FormItem>
          </Col>
          <Col span="12">
          <FormItem label="电子单元产品编号"  prop="electronicUnitNumber" label-position="left">
            <Input v-model="formModel.fields.electronicUnitNumber" disabled placeholder="请输入常用流量比" />
          </FormItem>
          </Col>
        </Row>
        <Row :gutter="32">
          <Col span="12">
          <FormItem label="基表批次编号"  prop="btid" label-position="left">
            <Input v-model="formModel.fields.btid" disabled placeholder="请输入常用流量" />
          </FormItem>
          </Col>
          <Col span="12">
          <FormItem label="基表批次号"  prop="batchNumber" label-position="left">
            <Input v-model="formModel.fields.batchNumber" disabled placeholder="请输入常用流量比" />
          </FormItem>
          </Col>
        </Row>



        <Row :gutter="32">
          <Col span="12">
          <FormItem label="生产日期" prop="dateOfManufacture" label-position="left">
            <Input v-model="formModel.fields.dateOfManufacture" placeholder="请输入生产日期" />
          </FormItem>
          </Col>

        </Row>
        <Row :gutter="32">
          <Col span="12">
          <FormItem label="查询电子单元产品编号">
            <Select v-model="formModel.fields.Ecutableindex"
                    filterable
                    clearable
                    remote
                    @on-change="handleEcutablekeyword"
                    :remote-method="handleLoadEcutableDataSource"
                    :loading="stores.worktask.sources.ecutableSources.loading"
                    placeholder="请选择电子单元产品编号...">
              <Option v-for="(item , Ecutableindex) in stores.worktask.sources.ecutableSources.data"
                      :value="Ecutableindex"
                      :label="item.electronicUnitNumber"
                      :key="Ecutableindex">
                ID:{{ item.ecuid }}||
                编号：{{ item.electronicUnitNumber }}||
                时间：{{ item.dateOfManufacture}}

              </Option>
            </Select>
          </FormItem>
          </Col>
          <Col span="12">
          <FormItem label="查询水表批次编号">
            <Select v-model="formModel.fields.Basetableindex"
                    filterable
                    clearable
                    remote
                    @on-change="handleBasetablekeyword"
                    :remote-method="handleLoadBasetableDataSource"
                    :loading="stores.worktask.sources.basetableSources.loading"
                    placeholder="请选择水表批次编号...">
              <Option v-for="(item , Basetableindex) in stores.worktask.sources.basetableSources.data"
                      :value="Basetableindex"
                      :label="item.batchNumber"
                      :key="Basetableindex">
                ID:{{ item.btid }}||
                编号：{{ item.batchNumber }}||
                时间：{{ item.dateOfManufacture}}

              </Option>
            </Select>
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
  getWorkTaskList,
  createWorkTask,
  loadWorkTask,
  editWorkTask,
  deleteWorkTask,
  batchCommand,
  gettimeList,
  } from "@/api/rbac/worktask";
  import { findEcutableDataSourceByKeyword } from "@/api/rbac/ECUTable";
  import { findbasetableDataSourceByKeyword } from "@/api/rbac/basetable";

export default {
    name: "rbac_worktask_page",
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
        title: "创建",
        mode: "create",
        selection: [],
        selectOption: {
          ecutable: {}
        },
        fields: {
          Ecutableindex: 0,
          Basetableindex:0,
          diid: "",
          eunumber: "",
          productModel: "LXSY-",
          ecuid:"",
          electronicUnitNumber:"",
          btid: "",
          batchNumber: "",
          dateOfManufacture: "2021-03-07",
         
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
        worktask: {
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
            ],
            ecutableSources: {
              loading: false,
              electronicUnitNumber:"",
              data: []
            },
            basetableSources: {
              loading: false,
              electronicUnitNumber: "",
              data: []
            }
          },
          columns: [
            { type: "selection", width: 50, key: "handle" },
            { title: "序号", key: "id", width: 50, sortable: true, ellipsis: true, tooltip: true },
            { title: "主题", key: "taskTheme", width: 150, sortable: true, ellipsis: true, tooltip: true },
            { title: "任务内容", key: "taskContent", width: 200, sortable: true, ellipsis: true, tooltip: true },
            { title: "类型", key: "workType",
            width: 100, sortable: true,
            render:(h,params)=>{
                        let workType = params.row.workType
                        if(workType==0){
                            workType = '重要任务'   
                        }else if(workType==1){
                            workType = '一般任务'
                        }else if(workType ==2){
                            workType = '次要任务'
                        }
                        return h('span',workType)
                    },
            },
            { title: "1", key: "no1", width: 30, ellipsis: true, tooltip: true },
            { title: "2", key: "no2", width: 30, ellipsis: true, tooltip: true },
            { title: "3", key: "no3", width: 30, ellipsis: true, tooltip: true },
            { title: "4", key: "no4", width: 30, ellipsis: true, tooltip: true },
            { title: "5", key: "no5", width: 30, ellipsis: true, tooltip: true },
            { title: "6", key: "no6", width: 30, ellipsis: true, tooltip: true },
            { title: "7", key: "no7", width: 30, ellipsis: true, tooltip: true },
            { title: "8", key: "no8", width: 30, ellipsis: true, tooltip: true },
            { title: "9", key: "no9", width: 30, ellipsis: true, tooltip: true },
            { title: "10", key: "no10", width: 30, ellipsis: true, tooltip: true },
            { title: "11", key: "no11", width: 30, ellipsis: true, tooltip: true },
            { title: "12", key: "no12", width: 30, ellipsis: true, tooltip: true },
            { title: "13", key: "no13", width: 30, ellipsis: true, tooltip: true },
            { title: "14", key: "no14", width: 30, ellipsis: true, tooltip: true },
            { title: "15", key: "no15", width: 30, ellipsis: true, tooltip: true },
            { title: "16", key: "no16", width: 30, ellipsis: true, tooltip: true },
            { title: "17", key: "no17", width: 30, ellipsis: true, tooltip: true },
            { title: "18", key: "no18", width: 30, ellipsis: true, tooltip: true },
            { title: "19", key: "no19", width: 30, ellipsis: true, tooltip: true },
            { title: "20", key: "no20", width: 30, ellipsis: true, tooltip: true },
            { title: "21", key: "no21", width: 30, ellipsis: true, tooltip: true },
            { title: "22", key: "no22", width: 30, ellipsis: true, tooltip: true },
            { title: "23", key: "no23", width: 30, ellipsis: true, tooltip: true },
            { title: "24", key: "no24", width: 30, ellipsis: true, tooltip: true },
            { title: "25", key: "no25", width: 30, ellipsis: true, tooltip: true },
            { title: "26", key: "no26", width: 30, ellipsis: true, tooltip: true },
            { title: "27", key: "no27", width: 30, ellipsis: true, tooltip: true },
            { title: "28", key: "no28", width: 30, ellipsis: true, tooltip: true },
            { title: "29", key: "no29", width: 30, ellipsis: true, tooltip: true },
            { title: "30", key: "no30", width: 30, ellipsis: true, tooltip: true },
            { title: "31", key: "no31", width: 30, ellipsis: true, tooltip: true },
            { title: "任务人", key: "taskPerson", width: 80, sortable: true, ellipsis: true, tooltip: true },
            { title: "联系电话", key: "telephone", width: 100, ellipsis: true, tooltip: true },
            { title: "任务时间", key: "taskTime", width: 150, sortable: true, ellipsis: true, tooltip: true },
            { title: "完成时间节点", key: "completionTime", width: 150,ellipsis: true, tooltip: true },
            
            { title: "进度偏离", key: "progressDeviation", width: 150, ellipsis: true, tooltip: true },
            { title: "情况说明", key: "informationNote", width: 150, ellipsis: true, tooltip: true },
            { title: "第三方配合事项", key: "thirdPartyCooperation", width: 150, ellipsis: true, tooltip: true },
            { title: "注意事项", key: "mattersNeedingAttention", width: 150,ellipsis: true, tooltip: true },
            { title: "项目经理", key: "projectManager", width: 100, ellipsis: true, tooltip: true },
            { title: "发布人", key: "publisher", width: 100,ellipsis: true, tooltip: true }
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
      return this.formModel.selection.map(x => x.diid);
    }
  },
  methods: {
    loadRoleList() {
      getWorkTaskList(this.stores.worktask.query).then(res => {
        this.stores.worktask.data = res.data.data;
        console.log(res.data.data )
        this.stores.worktask.query.totalCount = res.data.totalCount;
      });
      gettimeList(this.stores.worktask.query).then(res => { 
        this.stores.worktask.data = res.data.data;
        console.log(res.data.data )
        this.stores.worktask.query.totalCount = res.data.totalCount;
      });
    },
    loadtimeList(){
      gettimeList(this.stores.worktask.query).then(res => { 
        this.stores.worktask.data = res.data.data;
        console.log(res.data.data )
        this.stores.worktask.query.totalCount = res.data.totalCount;
      });
    },
    
    exportData() {
      this.$refs.tables.exportCsv({
        filename: "电子单元信息",
        original: false,
        columns: this.stores.worktask.columns,
        data: this.stores.worktask.data
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
      this.doLoadRole(params.row.diid);
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
      createWorkTask(this.formModel.fields).then(res => {
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
      editWorkTask(this.formModel.fields).then(res => {
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
    doLoadRole(diid) {
      loadWorkTask({ diid: diid }).then(res => {
        this.formModel.fields = res.data.data;
      });
    },
    handleDelete(params) {
      this.doDelete(params.row.diid);
    },
    doDelete(ids) {
      if (!ids) {
        this.$Message.warning("请选择至少一条数据");
        return;
      }
      deleteWorkTask(ids).then(res => {
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
      this.stores.worktask.query.currentPage = page;
      this.loadRoleList();
    },
    handlePageSizeChanged(pageSize) {
      this.stores.worktask.query.pageSize = pageSize;
      this.loadRoleList();
    },
    handleEcutablekeyword() {
      this.formModel.fields.ecuid = this.stores.worktask.sources.ecutableSources.data[this.formModel.fields.Ecutableindex].ecuid;
      this.formModel.fields.electronicUnitNumber = this.stores.worktask.sources.ecutableSources.data[this.formModel.fields.Ecutableindex].electronicUnitNumber;
      this.formModel.fields.eunumber = this.stores.worktask.sources.ecutableSources.data[this.formModel.fields.Ecutableindex].electronicUnitNumber;
      //console.log(this.formModel.fields.ecuid)
    },
    handleLoadEcutableDataSource(keyword) {
      this.stores.worktask.sources.ecutableSources.loading = true;
      let query = { keyword: keyword };
      findEcutableDataSourceByKeyword(query).then(res => {
 
        this.stores.worktask.sources.ecutableSources.data = res.data.data;
        this.stores.worktask.sources.ecutableSources.loading = false;
        
      });
    },
    handleBasetablekeyword() {
      this.formModel.fields.btid = this.stores.worktask.sources.basetableSources.data[this.formModel.fields.Basetableindex].btid;
      this.formModel.fields.batchNumber = this.stores.worktask.sources.basetableSources.data[this.formModel.fields.Basetableindex].batchNumber;
      //console.log(this.formModel.fields.ecuid)
    },
    handleLoadBasetableDataSource(keyword) {
      this.stores.worktask.sources.basetableSources.loading = true;
      let query = { keyword: keyword };
      findbasetableDataSourceByKeyword(query).then(res => {

        this.stores.worktask.sources.basetableSources.data = res.data.data;
        this.stores.worktask.sources.basetableSources.loading = false;

      });
    }
    
  },
  mounted() {
    this.loadRoleList();
  }
};</script>
