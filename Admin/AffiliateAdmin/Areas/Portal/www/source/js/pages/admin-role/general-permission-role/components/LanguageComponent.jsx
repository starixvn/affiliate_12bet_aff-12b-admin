import React, { PureComponent, Fragment } from 'react';
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';
import { Table, Row, Cell } from '@9softstudio/react-fixed-table-header';
import { Button } from 'reactstrap';

import { headerText, globalResource } from '../libs/constants';
import { fetchLanguageData } from '../actions';

export class CommonGridData extends PureComponent {
    constructor(props) {
        super(props);

        this.state = {
            isEdit: false,
        }
    }

    componentDidMount() {
        this.props.fetchLanguageData();
    }

    _renderHeader() {
        const languageList = [
            { MarketId: 1, MarketName: 'English' },
            { MarketId: 2, MarketName: 'English (Europe)' },
            { MarketId: 3, MarketName: 'English (India)' },
            { MarketId: 4, MarketName: 'Chinese (Simplified)' },
            { MarketId: 5, MarketName: 'Chinese (Traditional)' },
            { MarketId: 6, MarketName: 'Vietnamese' },
            { MarketId: 7, MarketName: 'Thai' },
            { MarketId: 8, MarketName: 'Indonesian' },
            { MarketId: 9, MarketName: 'Korean' },
        ];

        return (
            <Row key="theard">
                <Cell header={true} colWidth={250} title={headerText.Role}>{headerText.Role}</Cell>
                {/* {
                    languageList.map((item) => {
                        return
                        (<Cell header={true} colWidth={80} title={'English'}>{'English'}</Cell>)
                    })
                } */}
                <Cell header={true} colWidth={80} title={'English'}>{'English'}</Cell>
                <Cell header={true} colWidth={80} title={'English (Europe)'}>{'English (Europe)'}</Cell>
                <Cell header={true} colWidth={80} title={'English (India)'}>{'English (India)'}</Cell>
                <Cell header={true} colWidth={80} title={'Chinese (Simplified)'}>{'Chinese (Simplified)'}</Cell>
                <Cell header={true} colWidth={80} title={'Chinese (Traditional)'}>{'Chinese (Traditional)'}</Cell>
                <Cell header={true} colWidth={80} title={'Vietnamese'}>{'Vietnamese'}</Cell>
                <Cell header={true} colWidth={80} title={'Thai'}>{'Thai'}</Cell>
                <Cell header={true} colWidth={80} title={'Indonesian'}>{'Indonesian'}</Cell>
                <Cell header={true} colWidth={80} title={'Korean'}>{'Korean'}</Cell>
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
                <Cell className="text-center" colSpan="11">{globalResource.NoInfo}</Cell>
            </Row>);
        }

        return dataRows;
    }

    _buildDataRowView(dataItem, index) {
        return (
            <Row key={`dataRow-${dataItem.RoleId}`}>
                <Cell title={dataItem.RoleName}>{dataItem.RoleName}</Cell>
                <Cell className="text-center">
                    {
                        dataItem.IsES ? <span className="oi oi-check text-success"></span> : null
                    }
                </Cell>
                <Cell className="text-center">
                    {
                        dataItem.IsEU ? <span className="oi oi-check text-success"></span> : null
                    }
                </Cell>
                <Cell className="text-center">
                    {
                        dataItem.IsEI ? <span className="oi oi-check text-success"></span> : null
                    }
                </Cell>
                <Cell className="text-center">
                    {
                        dataItem.IsCS ? <span className="oi oi-check text-success"></span> : null
                    }
                </Cell>
                <Cell className="text-center">
                    {
                        dataItem.IsCH ? <span className="oi oi-check text-success"></span> : null
                    }
                </Cell>
                <Cell className="text-center">
                    {
                        dataItem.IsVN ? <span className="oi oi-check text-success"></span> : null
                    }
                </Cell>
                <Cell className="text-center">
                    {
                        dataItem.IsTH ? <span className="oi oi-check text-success"></span> : null
                    }
                </Cell>
                <Cell className="text-center">
                    {
                        dataItem.IsID ? <span className="oi oi-check text-success"></span> : null
                    }
                </Cell>
                <Cell className="text-center">
                    {
                        dataItem.IsKO ? <span className="oi oi-check text-success"></span> : null
                    }
                </Cell>
            </Row>
        )
    }

    _buildDataRowEdit(dataItem) {
        return (
            <Row key={`dataRow-${dataItem.RoleId}`}>
                <Cell title={dataItem.RoleName}>{dataItem.RoleName}</Cell>
                <Cell className="text-center">
                    <input type="checkbox" checked={dataItem.IsES} onChange={this._handleChange} />
                </Cell>
                <Cell className="text-center">
                    <input type="checkbox" checked={dataItem.IsEU} onChange={this._handleChange} />
                </Cell>
                <Cell className="text-center">
                    <input type="checkbox" checked={dataItem.IsEI} onChange={this._handleChange} />
                </Cell>
                <Cell className="text-center">
                    <input type="checkbox" checked={dataItem.IsCS} onChange={this._handleChange} />
                </Cell>
                <Cell className="text-center">
                    <input type="checkbox" checked={dataItem.IsCH} onChange={this._handleChange} />
                </Cell>
                <Cell className="text-center">
                    <input type="checkbox" checked={dataItem.IsVN} onChange={this._handleChange} />
                </Cell>
                <Cell className="text-center">
                    <input type="checkbox" checked={dataItem.IsTH} onChange={this._handleChange} />
                </Cell>
                <Cell className="text-center">
                    <input type="checkbox" checked={dataItem.IsID} onChange={this._handleChange} />
                </Cell>
                <Cell className="text-center">
                    <input type="checkbox" checked={dataItem.IsKO} onChange={this._handleChange} />
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
                        minWidth={1000}
                        autoWidth={true}
                        bodyHeight={300}
                        header={this._renderHeader()}
                        body={this._renderDataSection(gridData)}
                        containerPadding={52}
                    />
                </div>
            </div>
        );
    }
}


function mapStateToProps({ userId, generalPermissionRole }) {
    const { gridData } = generalPermissionRole;
    return Object.assign({}, { userId, gridData });
}

function mapDispatchToProps(dispatch) {
    return bindActionCreators({ fetchLanguageData }, dispatch);
}

export default connect(mapStateToProps, mapDispatchToProps)(CommonGridData);