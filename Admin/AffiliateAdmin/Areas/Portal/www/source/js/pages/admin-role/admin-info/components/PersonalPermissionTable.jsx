import React, { PureComponent, Fragment } from 'react';
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';
import { Table, Row, Cell } from '@9softstudio/react-fixed-table-header';
import { Button } from 'reactstrap';

import { headerText, globalResource } from '../libs/constants';
import { setRolePermission } from '../actions';

export class PersonalPermissionTable extends PureComponent {
    constructor(props) {
        super(props);
    }

    // componentDidMount() {
    //     const { isGeneral, userId } = this.props;

    //     this.props.fetchData(isGeneral, userId);
    // }

    // componentDidUpdate(prevProps) {
    //     const { isGeneral, userId } = this.props;

    //     if (isGeneral !== prevProps.isGeneral) {
    //         this.props.fetchData(isGeneral, userId);
    //     }
    // }

    _renderHeader() {

        return [
            <Row key="theard">
                <Cell header={true} colWidth={300} rowSpan="2" title={headerText.Function}>{headerText.Function}</Cell>
                <Cell header={true} colWidth={200} colSpan="2" title={headerText.IndividualPermission}>{headerText.IndividualPermission}</Cell>
                <Cell header={true} colWidth={200} colSpan="2" title={headerText.RolePermission}>{headerText.RolePermission}</Cell>
            </Row>,
            <Row key="sub-theard">
                <Cell header={true} colWidth={100} title={headerText.Read}>{headerText.Read}</Cell>
                <Cell header={true} colWidth={100} title={headerText.Host}>{headerText.Host}</Cell>
                <Cell header={true} colWidth={100} title={headerText.Read}>{headerText.Read}</Cell>
                <Cell header={true} colWidth={100} title={headerText.Host}>{headerText.Host}</Cell>
            </Row>
        ];
    }

    _renderDataSection(dataList) {
        const dataRows = [];

        if (dataList && dataList.length) {
            const { isCreateNew, isEdit } = this.props;

            for (let i = 0; i < dataList.length; i++) {
                const dataItem = dataList[i];
                dataRows.push(isCreateNew || isEdit ? this._buildDataRowEdit(dataItem, i + 1) : this._buildDataRowView(dataItem, i + 1));
            }
        }
        else {
            dataRows.push(<Row>
                <Cell className="text-center" colSpan="5">{globalResource.NoInfo}</Cell>
            </Row>);
        }

        return dataRows;
    }

    _buildDataRowView(dataItem, index) {
        return (
            <Row key={`dataRow-${index}`}>
                <Cell title={dataItem.Name}>{dataItem.Name}</Cell>
                <Cell className="text-center" title={headerText.View}>
                    {
                        dataItem.IsView && <span className="oi oi-check text-primary"></span>
                    }
                </Cell>
                <Cell className="text-center" title={headerText.View}>
                    {
                        dataItem.IsEdit && <span className="oi oi-check text-primary"></span>
                    }
                </Cell>
                <Cell className="text-center" title={headerText.Edit}>
                    {
                        dataItem.IsView && <span className="oi oi-check text-primary"></span>
                    }
                </Cell>
                <Cell className="text-center" title={headerText.Edit}>
                    {
                        dataItem.IsEdit && <span className="oi oi-check text-primary"></span>
                    }
                </Cell>
            </Row>
        )
    }

    _buildDataRowEdit(dataItem, index) {
        return (
            <Row key={`dataRow-${index}`}>
                <Cell title={dataItem.Name}>{dataItem.Name}</Cell>
                <Cell className="text-center" title={headerText.View}>
                    <input type="checkbox" checked={dataItem.IsView} onChange={this._handleChange} />
                </Cell>
                <Cell className="text-center" title={headerText.View}>
                    <input type="checkbox" checked={dataItem.IsEdit} onChange={this._handleChange} />
                </Cell>
                <Cell className="text-center" title={headerText.Edit}>
                    <input type="checkbox" checked={dataItem.IsView} onChange={this._handleChange} />
                </Cell>
                <Cell className="text-center" title={headerText.Edit}>
                    <input type="checkbox" checked={dataItem.IsEdit} onChange={this._handleChange} />
                </Cell>
            </Row>
        )
    }

    render() {
        const { rolePermission } = this.props;

        return rolePermission && (
            <div className="data-content">
                <div className="table-responsive table-striped my-2">
                    <Table
                        maxWidth={1000}
                        bodyHeight={300}
                        header={this._renderHeader()}
                        body={this._renderDataSection(rolePermission)}
                        containerPadding={20}
                    />
                </div>
            </div>
        );
    }
}


function mapStateToProps({ adminInfo }) {
    const { rolePermission, isCreateNew, isEdit } = adminInfo;
    return Object.assign({}, { rolePermission, isCreateNew, isEdit });
}

function mapDispatchToProps(dispatch) {
    return bindActionCreators({ setRolePermission }, dispatch);
}

export default connect(mapStateToProps, mapDispatchToProps)(PersonalPermissionTable);