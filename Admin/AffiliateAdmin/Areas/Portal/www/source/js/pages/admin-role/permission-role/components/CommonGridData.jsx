import React, { PureComponent, Fragment } from 'react';
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';
import { Table, Row, Cell } from '@9softstudio/react-fixed-table-header';
import { Button } from 'reactstrap';

import { headerText, globalResource } from '../libs/constants';
import { fetchData } from '../actions';

export class CommonGridData extends PureComponent {
    constructor(props) {
        super(props);
    }

    componentDidMount() {
        const { isGeneral, userId } = this.props;

        this.props.fetchData(isGeneral, userId);
    }

    componentDidUpdate(prevProps) {
        const { isGeneral, userId } = this.props;

        if (isGeneral !== prevProps.isGeneral) {
            this.props.fetchData(isGeneral, userId);
        }
    }

    _renderHeader() {
        const { isGeneral } = this.props;
        const title = isGeneral ? headerText.Function : headerText.Language;

        return isGeneral ? [
                <Row key="theard">
                    <Cell header={true} colWidth={50} rowSpan="2" title={headerText.Index}>{headerText.Index}</Cell>
                    <Cell header={true} colWidth={300} rowSpan="2" title={title}>{title}</Cell>
                    <Cell header={true} colWidth={200} colSpan="2" title={headerText.IndividualPermission}>{headerText.IndividualPermission}</Cell>
                    <Cell header={true} colWidth={200} colSpan="2" title={headerText.RolePermission}>{headerText.RolePermission}</Cell>
                </Row>,
                <Row key="sub-theard">
                    <Cell header={true} colWidth={100} title={headerText.Read}>{headerText.Read}</Cell>
                    <Cell header={true} colWidth={100} title={headerText.Host}>{headerText.Host}</Cell>
                    <Cell header={true} colWidth={100} title={headerText.Read}>{headerText.Read}</Cell>
                    <Cell header={true} colWidth={100} title={headerText.Host}>{headerText.Host}</Cell>
                </Row>
        ] : [
                <Row key="theard">
                    <Cell header={true} colWidth={50} title={headerText.Index}>{headerText.Index}</Cell>
                    <Cell header={true} colWidth={700} title={title}>{title}</Cell>
                    <Cell header={true} colWidth={250} title={headerText.View}>{headerText.View}</Cell>
                </Row>
            ];
    }

    _renderDataSection(dataList) {
        const dataRows = [];
        const { isGeneral } = this.props;

        if (dataList && dataList.length) {
            for (let i = 0; i < dataList.length; i++) {
                const dataItem = dataList[i];
                dataRows.push(this._buildDataRow(dataItem, i + 1, isGeneral));
            }
        }
        else {
            dataRows.push(<Row>
                <Cell className="text-center" colSpan={isGeneral ? 7 : 6}>{globalResource.NoInfo}</Cell>
            </Row>);
        }

        return dataRows;
    }

    _buildDataRow(dataItem, index, isGeneral) {
        return (
            <Row key={`dataRow-${index}`}>
                <Cell title={index} className="text-center">{index}</Cell>
                <Cell title={dataItem.Name}>{dataItem.Name}</Cell>
                <Cell className="text-center" title={headerText.View}>
                    <Button color="link p-0" onClick={() => this._handleEdit(dataItem.Id)}>
                        <span className={`oi ${dataItem.IsView ? "oi-check text-success" : "oi-x text-danger"}`}></span>
                    </Button>
                </Cell>
                {
                    isGeneral && (
                        <Fragment>
                            <Cell className="text-center" title={headerText.View}>
                                <Button color="link p-0" onClick={() => this._handleEdit(dataItem.Id)}>
                                    <span className={`oi ${dataItem.IsView ? "oi-check text-success" : "oi-x text-danger"}`}></span>
                                </Button>
                            </Cell>
                            <Cell className="text-center" title={headerText.Edit}>
                                <Button color="link p-0" onClick={() => this._handleEdit(dataItem.Id)}>
                                    <span className={`oi ${dataItem.IsEdit ? "oi-check text-success" : "oi-x text-danger"}`}></span>
                                </Button>
                            </Cell>
                            <Cell className="text-center" title={headerText.Edit}>
                                <Button color="link p-0" onClick={() => this._handleEdit(dataItem.Id)}>
                                    <span className={`oi ${dataItem.IsEdit ? "oi-check text-success" : "oi-x text-danger"}`}></span>
                                </Button>
                            </Cell>
                        </Fragment>
                    )
                }
            </Row>
        )
    }

    render() {
        const { gridData } = this.props;

        return gridData && (
            <div className="data-content">
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


function mapStateToProps({ userId, permissionRole }) {
    const { gridData, isGeneral } = permissionRole;
    return Object.assign({}, { userId, gridData, isGeneral });
}

function mapDispatchToProps(dispatch) {
    return bindActionCreators({ fetchData }, dispatch);
}

export default connect(mapStateToProps, mapDispatchToProps)(CommonGridData);