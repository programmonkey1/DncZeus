
<template>
  <div class="form-style">
    <h1> 设备安装位置录入</h1>
    <hr />
    <Row :gutter="32">
      <Col span="12">
      <Button icon="md-create"
              type="primary"
              @click="handleShowCreateWindow"
              title="产品属性">
        产品属性
      </Button>
      </Col>
      <Col span="12">
      </Col>
    </Row>

    <Form :model="formModel.fields" ref="formRole" :rules="formModel.rules" label-position="left">
      <Row :gutter="32">
        <Col span="12">
        <FormItem label="查询产品编号">
          <Select v-model="formModel.fields.Ecutableindex">
            <Option value="beijing">New York</Option>
            <Option value="shanghai">London</Option>
            <Option value="shenzhen">Sydney</Option>
            </Option>
          </Select>
        </FormItem>
        </Col>
        <Col span="12">
        <FormItem label="查询缴费编号">
          <Select v-model="formModel.fields.Basetableindex">
            <Option value="beijing">New York</Option>
            <Option value="shanghai">London</Option>
            <Option value="shenzhen">Sydney</Option>
            </Option>
          </Select>
        </FormItem>
        </Col>
      </Row>
      <Row :gutter="52">
        <Col span="12">
        <FormItem label="安装日期" prop="dateOfManufacture" label-position="left">
          <DatePicker placeholder="请输入安装日期" v-model="formModel.fields.dateOfManufacture" />
        </FormItem>
        </Col>
        <Col span="12">
        <FormItem label="设备状态" label-position="left">
          <i-switch size="large" v-model="formModel.fields.status" :true-value="1" :false-value="0">
            <span slot="open">正常</span>
            <span slot="close">禁用</span>
          </i-switch>
        </FormItem>
        </Col>
      </Row>
      <Row :gutter="52">
        <Col span="12">
        <FormItem label="设备标识" prop="ecuid" label-position="left">
          <Input v-model="formModel.fields.ecuid" disabled placeholder="请输入参数规格" />
        </FormItem>
        </Col>
        <Col span="12">
        <FormItem label="位置标识" prop="electronicUnitNumber" label-position="left">
          <Input v-model="formModel.fields.electronicUnitNumber" disabled placeholder="请输入常用流量比" />
        </FormItem>
        </Col>
      </Row>
      <Row :gutter="52">
        <Col span="8">
        <FormItem label="区域名称" prop="btid" label-position="left">
          <Input v-model="formModel.fields.btid" disabled placeholder="请输入常用流量" />
        </FormItem>
        </Col>
        <Col span="8">
        <FormItem label="小区名称" prop="batchNumber" label-position="left">
          <Input v-model="formModel.fields.batchNumber" disabled placeholder="请输入常用流量比" />
        </FormItem>
        </Col>
        <Col span="8">
        <FormItem label="小区地址" prop="btid" label-position="left">
          <Input v-model="formModel.fields.btid" disabled placeholder="请输入常用流量" />
        </FormItem>
        </Col>
      </Row>
      <Row :gutter="52">
        <Col span="12">
        <FormItem label="楼幢名称" prop="batchNumber" label-position="left">
          <Input v-model="formModel.fields.batchNumber" disabled placeholder="请输入常用流量比" />
        </FormItem>
        </Col>
        <Col span="12">
        <FormItem label="单元名称" prop="batchNumber" label-position="left">
          <Input v-model="formModel.fields.batchNumber" disabled placeholder="请输入常用流量比" />
        </FormItem>
        </Col>
      </Row>

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
 
    <Drawer :title="formTitle"
            v-model="formModel.opened"
            width="400"
            :mask-closable="true"
            :mask="true"
            :styles="styles">
      <div class="dev-style">
        <template>
          <Card style="width:350px">
            <p slot="title">
              <Icon type="ios-film-outline"></Icon>
              电子单元属性
            </p>

            <li>
              <a>设备标识：</a><span>{{formModel.fields.ecuid}}</span>
            </li>
            <li>
              <a>设备编号： </a><span>{{formModel.fields.electronicUnitNumber}}</span>
            </li>
            <li>
              <a>产品型号： </a><span>{{formModel.fields.productModel}}</span>
            </li>
            <li>
              <a>生产日期：</a><span>{{formModel.fields.dateOfManufacture}}</span>
            </li>
            <li>
              <a>程序版本： </a><span>{{formModel.fields.programVersion}}</span>
            </li>
            <li>
              <a>通讯方式：</a><span>{{formModel.fields.communicationMode}}</span>
            </li>
            <li>
              <a>平均电流： </a><span>{{formModel.fields.averageWorkingCurrent}}</span>
            </li>
            <li>
              <a>防护等级：</a><span>{{formModel.fields.protectionLevel}}</span>
            </li>
            <li>
              <a> 信号强度： </a><span>{{formModel.fields.signalIntensity}}</span>
            </li>
            <li>
              <a> IMSI：</a><span>{{formModel.fields.imsi}}</span>
            </li>
            <li>
              <a>IMEI：</a><span>{{formModel.fields.imei}}</span>
            </li>
            <li>
              <a>干扰状态：</a><span>{{formModel.fields.magneticInterferenceStatus}}</span>
            </li>
            <li>
              <a> 电池型号：</a><span>{{formModel.fields.batteryModel}}</span>
            </li>
            <li>
              <a>电压状态： </a><span>{{formModel.fields.voltageState}}</span>
            </li>
            <li>
              <a> 电压：</a><span>{{formModel.fields.voltage}}</span>
            </li>
            <li>
              <a>设备状态：</a><span>{{formModel.fields.status}}</span>
            </li>
            <li><a>备注：</a><span>{{formModel.fields.remarks}}</span></li>
          </Card>
        </template>
      </div>
      <div class="dev-style">
        <template>
          <Card style="width:350px">
            <p slot="title">
              <Icon type="ios-film-outline"></Icon>
              基表属性
            </p>

            <li>
              <a>基表标识：</a><span>{{formModel.fields.ecuid}}</span>
            </li>
            <li>
              <a>批次编号： </a><span>{{formModel.fields.electronicUnitNumber}}</span>
            </li>
            <li>
              <a>钢印编号： </a><span>{{formModel.fields.productModel}}</span>
            </li>
            <li>
              <a>参数规格：</a><span>{{formModel.fields.dateOfManufacture}}</span>
            </li>
            <li>
              <a>安装方式： </a><span>{{formModel.fields.programVersion}}</span>
            </li>
            <li>
              <a>常用流量：</a><span>{{formModel.fields.communicationMode}}</span>
            </li>
            <li>
              <a>常用流量比： </a><span>{{formModel.fields.averageWorkingCurrent}}</span>
            </li>
            <li>
              <a>初始流量：</a><span>{{formModel.fields.protectionLevel}}</span>
            </li>
            <li>
              <a> 最小度数： </a><span>{{formModel.fields.signalIntensity}}</span>
            </li>
            <li>
              <a> 最大度数：</a><span>{{formModel.fields.imsi}}</span>
            </li>
            <li>
              <a>最大压力：</a><span>{{formModel.fields.imei}}</span>
            </li>
            <li>
              <a>最大温度：</a><span>{{formModel.fields.magneticInterferenceStatus}}</span>
            </li>
            <li>
              <a> 环境温度：</a><span>{{formModel.fields.batteryModel}}</span>
            </li>
            <li>
              <a>环境湿度： </a><span>{{formModel.fields.voltageState}}</span>
            </li>
            <li>
              <a> 执行标准：</a><span>{{formModel.fields.voltage}}</span>
            </li>
            <li>
              <a>生产日期：</a><span>{{formModel.fields.status}}</span>
            </li>
            <li>
              <a>生产数量：</a><span>{{formModel.fields.status}}</span>
            </li>
            <li>
              <a>设备状态：</a><span>{{formModel.fields.status}}</span>
            </li>
            <li><a>备注：</a><span>{{formModel.fields.remarks}}</span></li>
          </Card>
        </template>
       </div>
    </Drawer>
  </div>
</template>
<script>
  export default {
    name: 'rbac_equipmentinput_page',
    data() {
      return {
        formItem: {
          input: '',
          select: '',
          radio: 'male',
          checkbox: [],
          switch: true,
          date: '',
          time: '',
          slider: [20, 50],
          textarea: ''
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
            Basetableindex: 0,
            diid: "1",
            eunumber: "1",
            productModel: "LXSY-",
            ecuid: "1",
            electronicUnitNumber: "1",
            btid: "1",
            batchNumber: "1",
            dateOfManufacture: "2021-03-07",
            remarks: "1",
            status: 1,
            isDeleted: 0,                  
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
        }
      }
    },
    methods: {
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
      handleShowCreateWindow() {
        this.handleSwitchFormModeToCreate();
        this.handleOpenFormWindow(); 
      }
     
    }
   
  }

</script>
<style lang="less">
  .form-style {
    font-size: 35px;
    margin: 0 auto;
    width: 800px;
    border: 1px solid #f2f2f2;
    border-radius: 5px;
    background-color: #f2f2f2;
    padding: 20px;
    box-shadow: 10px 10px 5px #888888;
  }
  .dev-style {
    box-shadow: 10px 10px 5px #888888;
  }
  h1 {
    text-shadow: 5px 5px 5px #f2f2f2;
    font-size: 35px;
  }
  .h {
    border-bottom: 1px solid black;
  }
</style>


