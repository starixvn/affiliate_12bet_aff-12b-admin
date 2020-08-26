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

        this.state = {
            isEdit: false,
        }
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
            const { isEdit } = this.state;

            for (let i = 0; i < dataList.length; i++) {
                const dataItem = dataList[i];
                dataRows.push(isEdit ? this._buildDataRowEdit(dataItem, i + 1, isGeneral) : this._buildDataRowView(dataItem, i + 1, isGeneral));
            }
        }
        else {
            dataRows.push(<Row>
                <Cell className="text-center" colSpan={isGeneral ? 7 : 6}>{globalResource.NoInfo}</Cell>
            </Row>);
        }

        return dataRows;
    }

    _buildDataRowView(dataItem, index, isGeneral) {
        return (
            <Row key={`dataRow-${index}`}>
                <Cell title={index} className="text-center">{index}</Cell>
                <Cell title={dataItem.Name}>{dataItem.Name}</Cell>
                <Cell className="text-center" title={headerText.View}>
                    {
                        dataItem.IsView && <span className="oi oi-check text-primary"></span>
                    }
                </Cell>
                {
                    isGeneral && (
                        <Fragment>
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
                        </Fragment>
                    )
                }
            </Row>
        )
    }

    _buildDataRowEdit(dataItem, index, isGeneral) {
        return (
            <Row key={`dataRow-${index}`}>
                <Cell title={index} className="text-center">{index}</Cell>
                <Cell title={dataItem.Name}>{dataItem.Name}</Cell>
                <Cell className="text-center" title={headerText.View}>
                    <input type="checkbox" checked={dataItem.IsView} onChange={this._handleChange} />
                </Cell>
                {
                    isGeneral && (
                        <Fragment>
                            <Cell className="text-center" title={headerText.View}>
                                <input type="checkbox" color="success" checked={dataItem.IsEdit} onChange={this._handleChange} />
                            </Cell>
                            <Cell className="text-center" title={headerText.Edit}>
                                <input type="checkbox" checked={dataItem.IsView} onChange={this._handleChange} />
                            </Cell>
                            <Cell className="text-center" title={headerText.Edit}>
                                <input type="checkbox" checked={dataItem.IsEdit} onChange={this._handleChange} />
                            </Cell>
                        </Fragment>
                    )
                }
            </Row>
        )
    }

    render() {
        const { gridData } = this.props;
        const { isEdit } = this.state;

        return gridData && (
            <div className="data-content">
                <div className="my-2">
                    {
                        isEdit ? (
                            <Fragment>
                                <Button outline type="button" color="danger" onClick={() => this.setState({ isEdit: false })}>Cancel<span className="oi oi-x ml-2"></span></Button>
                                <Button outline type="button" className="ml-2" onClick={this._handleSubmit}>Save<span className="oi oi-task ml-2"></span></Button>
                            </Fragment>
                        ): (
                            <Button outline type = "button" onClick = {() => this.setState({isEdit: true })}> Edit < span className="oi oi-pencil ml-2"></span></Button>
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


function mapStateToProps({ userId, permissionRole }) {
    const { gridData, isGeneral } = permissionRole;
    return Object.assign({}, { userId, gridData, isGeneral });
}

function mapDispatchToProps(dispatch) {
    return bindActionCreators({ fetchData }, dispatch);
}

export default connect(mapStateToProps, mapDispatchToProps)(CommonGridData);