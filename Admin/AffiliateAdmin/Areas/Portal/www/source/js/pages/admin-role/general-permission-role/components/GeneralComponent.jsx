import React, { PureComponent, Fragment } from 'react';
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';
import { Table, Row, Cell } from '@9softstudio/react-fixed-table-header';
import { Button } from 'reactstrap';

import { headerText, globalResource } from '../libs/constants';
import { fetchData } from '../actions';

export class GeneralComponent extends PureComponent {
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
        return (
            <Row key="theard">
                <Cell header={true} colWidth={600} title={headerText.Function}>{headerText.Function}</Cell>
                <Cell header={true} colWidth={200} title={headerText.Read}>{headerText.Read}</Cell>
                <Cell header={true} colWidth={200} title={headerText.Host}>{headerText.Host}</Cell>
            </Row>
        );
    }

    _renderDataSection(dataList) {
        const dataRows = [];

        if (dataList && dataList.length) {
            const { isEdit } = this.state;

            for (let i = 0; i < dataList.length; i++) {
                const dataItem = dataList[i];
                dataRows.push(isEdit ? this._buildDataRowEdit(dataItem) : this._buildDataRowView(dataItem));
            }
        }
        else {
            dataRows.push(<Row>
                <Cell className="text-center" colSpan="3">{globalResource.NoInfo}</Cell>
            </Row>);
        }

        return dataRows;
    }

    _renderChildren(dataItem) {
        return (
            <Row key={`dataRow-${dataItem.FuncId}`}>
                <Cell title={dataItem.FuncName}>{dataItem.FuncName}</Cell>
                <Cell className="text-center" title={headerText.Read}>
                    {
                        dataItem.IsRead ? <span className="oi oi-check text-success"></span> : null
                    }
                </Cell>

                <Cell className="text-center" title={headerText.Host}>
                    {
                        dataItem.IsHost ? <span className="oi oi-check text-success"></span> : null
                    }
                </Cell>
            </Row>
        )
    }

    _buildDataRowView(dataItem) {
        return (
            <Row key={`dataRow-${dataItem.RoleId}`}>
                <Cell title={dataItem.RoleName}>{dataItem.RoleName}</Cell>
                <Cell className="text-center" title={headerText.Read}>
                    {
                        dataItem.IsRead ? <span className="oi oi-check text-success"></span> : null
                    }
                </Cell>

                <Cell className="text-center" title={headerText.Host}>
                    {
                        dataItem.IsHost ? <span className="oi oi-check text-success"></span> : null
                    }
                </Cell>
            </Row>
        )
    }

    _buildDataRowEdit(dataItem) {
        return (
            <Row key={`dataRow-${dataItem.RoleId}`}>
                <Cell title={dataItem.RoleName}>{dataItem.RoleName}</Cell>
                <Cell className="text-center" title={headerText.View}>
                    <input type="checkbox" checked={dataItem.IsRead} onChange={this._handleChange} />
                </Cell>
                <Cell className="text-center" title={headerText.Edit}>
                    <input type="checkbox" checked={dataItem.IsHost} onChange={this._handleChange} />
                </Cell>
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