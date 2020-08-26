import React, { PureComponent, Fragment } from 'react';
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';
import { Table, Row, Cell } from '@9softstudio/react-fixed-table-header';
import { Button } from 'reactstrap';

import { headerText, globalResource, management } from '../libs/constants';
import { fetchData, roleList, fakeFunctionList } from '../actions';

export class GeneralComponent extends PureComponent {
    constructor(props) {
        super(props);

        this.state = {
            isEdit: false,
            expandedRole: 0
        }
    }

    componentDidMount() {
        const { isGeneral, userId } = this.props;

        //this.props.fetchData(isGeneral, userId);
    }

    componentDidUpdate(prevProps) {
        const { isGeneral, userId } = this.props;

        if (isGeneral !== prevProps.isGeneral) {
            this.props.fetchData(isGeneral, userId);
        }
    }

    _renderHeader() {
        return (
            <Row key="theard">
                <Cell header={true} colWidth={600}>{headerText.Function}</Cell>
                <Cell header={true} colWidth={200}>{headerText.Read}</Cell>
                <Cell header={true} colWidth={200}>{headerText.Host}</Cell>
            </Row>
        );
    }

    _renderDataSection(dataList) {
        const dataRows = [];

        if (dataList && dataList.length) {
            const { isEdit, expandedRole } = this.state;

            for (let i = 0; i < dataList.length; i++) {
                const dataItem = dataList[i];
                dataRows.push(this._buildDataRowView(dataItem, expandedRole, isEdit));
            }
        }
        else {
            dataRows.push(<Row>
                <Cell className="text-center" colSpan="3">{globalResource.NoInfo}</Cell>
            </Row>);
        }

        return dataRows;
    }

    _buildDataRowView(dataItem, expandedRole, isEdit) {
        const children = fakeFunctionList.filter(f => f.RoleId === dataItem.RoleId);
        const isExpand = expandedRole === dataItem.RoleId;

        return (
            <Fragment>
                <Row key={`dataRow-${dataItem.RoleId}`}>
                    <Cell colSpan="3">
                        <div className="d-flex">
                            <Button className="py-0 th-wrapper" color="link" onClick={() => this.setState({ expandedRole: isExpand ? 0 : dataItem.RoleId })}>
                                <div className="icon-container">
                                    <span className={`caret ${isExpand ? 'expand current' : 'collapsed'}`}></span>
                                </div>
                                <span>{dataItem.RoleName}</span>
                            </Button>
                            {
                                dataItem.RoleId !== management && (
                                    isEdit ? (
                                        <Fragment>
                                            <a href="javascript:;" onClick={() => this.setState({ isEdit: false })}>< span className="oi oi-x text-danger"></span></a>
                                            <a href="javascript:;" onClick={() => this.setState({ isEdit: false })}>< span className="oi oi-check text-success ml-2"></span></a>
                                        </Fragment>
                                    ) : (
                                            <a href="javascript:;" onClick={() => this.setState({ expandedRole: dataItem.RoleId, isEdit: true })}>< span className="oi oi-pencil"></span></a>
                                        )
                                )
                            }
                        </div>
                    </Cell>
                </Row>
                {
                    children.length && isExpand && this._renderChildrenDataSection(children, isEdit)
                }
            </Fragment >
        )
    }

    _renderChildrenDataSection(dataList, isEdit) {
        const dataRows = [];
        const parentFunction = dataList.filter(s => s.FuncId === s.ParentId);
        const subFunction = dataList.filter(s => s.FuncId !== s.ParentId);

        for (let i = 0; i < parentFunction.length; i++) {
            const dataItem = parentFunction[i];
            dataRows.push(this._buildDataRowChildren(dataItem, subFunction, isEdit));
        }

        return dataRows;
    }

    _buildDataRowChildren(dataItem, dataList, isEdit) {

        return (
            <Fragment>
                <Row key={`dataRow-${dataItem.FuncId}`} className="current">
                    <Cell className="pl-2" title={dataItem.FuncName}>{dataItem.FuncName}</Cell>
                    <Cell className="text-center">
                        {
                            isEdit ? <input type="checkbox" checked={dataItem.IsRead} onChange={this._handleChange} /> : <span className="oi oi-check text-primary"></span>
                        }
                    </Cell>

                    <Cell className="text-center">
                        {
                            isEdit ? <input type="checkbox" checked={dataItem.IsHost} onChange={this._handleChange} /> : <span className="oi oi-check text-primary"></span>
                        }
                    </Cell>
                </Row>
                {
                    dataList.length && this._renderSubChildrenDataSection(dataItem, dataList, isEdit)
                }
            </Fragment>
        )
    }

    _renderSubChildrenDataSection(dataItem, dataList, isEdit, isRecursive) {
        const dataRows = [];
        const parentFunction = dataList.filter(s => s.ParentId === dataItem.FuncId);
        const subFunction = dataList.filter(s => s.ParentId !== dataItem.FuncId);

        for (let i = 0; i < parentFunction.length; i++) {
            const dataItem = parentFunction[i];
            dataRows.push(this._buildDataRowSubChildren(dataItem, subFunction, isEdit, isRecursive));
        }

        return dataRows;
    }

    _buildDataRowSubChildren(dataItem, dataList, isEdit, isRecursive) {

        return (
            <Fragment>
                <Row key={`dataRow-${dataItem.FuncId}`} className="current">
                    <Cell className={isRecursive ? "pl-5" : "pl-4"} title={dataItem.FuncName}>{dataItem.FuncName}</Cell>
                    <Cell className="text-center">
                        {
                            isEdit ? <input type="checkbox" checked={dataItem.IsRead} onChange={this._handleChange} /> : <span className="oi oi-check text-primary"></span>
                        }
                    </Cell>

                    <Cell className="text-center">
                        {
                            isEdit ? <input type="checkbox" checked={dataItem.IsHost} onChange={this._handleChange} /> : <span className="oi oi-check text-primary"></span>
                        }
                    </Cell>
                </Row>
                {
                    dataList.length && this._renderSubChildrenDataSection(dataItem, dataList, isEdit, true)
                }
            </Fragment>
        )
    }

    render() {
        //const { gridData } = this.props;
        const gridData = roleList;
        const { isEdit } = this.state;

        return gridData && (
            <div className="data-content general-permission-role">
                <div className="my-2">
                    {
                        isEdit ? (
                            <Fragment>
                                <Button outline type="button" color="danger" onClick={() => this.setState({ isEdit: false })}>Cancel<span className="oi oi-x ml-2"></span></Button>
                                <Button outline type="button" className="ml-2" onClick={this._handleSubmit}>Save<span className="oi oi-task ml-2"></span></Button>
                            </Fragment>
                        ) : (
                                <Button outline type="button" onClick={() => this.setState({ isEdit: true })}> Edit < span className="oi oi-pencil ml-2"></span></Button>
                            )
                    }
                </div>
                <div className="table-responsive table-striped my-2">
                    <Table
                        maxWidth={1000}
                        bodyHeight={300}
                        header={this._renderHeader()}
                        body={this._renderDataSection(gridData)}
                        containerPadding={20}
                    />
                </div>
            </div>
        );
    }
}


function mapStateToProps({ generalPermissionRole }) {
    const { gridData } = generalPermissionRole;
    return Object.assign({}, { gridData });
}

function mapDispatchToProps(dispatch) {
    return bindActionCreators({ fetchData }, dispatch);
}

export default connect(mapStateToProps, mapDispatchToProps)(GeneralComponent);