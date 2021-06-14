import React, { PureComponent, Fragment } from 'react';
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';
import { Button } from 'reactstrap';
import { Table, Row, Cell } from '@9softstudio/react-fixed-table-header';

import { headerText, management, messages } from '../libs/constants';
import { getMarketListByRole, toggleLoader, setGridData, updateMarketInfoList } from '../actions';
import NotificationPopup from '../../../common/components/NotificationPopup';
import { updateObjectInArray } from '../../../../modules/dataHandler';

export class LanguageComponent extends PureComponent {
    constructor(props) {
        super(props);

        this.state = {
            isEdit: false,
            gridData: null,
            messagePopup: '',
            isConfirmPopup: false
        }
    }

    componentDidMount() {
        this._handleGetMarketList();
    }

    _handleGetMarketList = async () => {
        this.props.toggleLoader(true);

        const marketList = await this.props.getMarketListByRole();
        const newMarketList = this._convertMarketList(marketList);

        this.props.setGridData(newMarketList);
        this.setState({ gridData: newMarketList });

        this.props.toggleLoader(false);
    }

    _convertMarketList = (marketList) => {
        const { marketIdList, roleList } = this.props;

        return marketIdList && roleList.map(item => {
            const marketOfRoleList = marketList.filter(m => m.RoleId === item.RoleId);

            let newMarketItem = { ...item };

            for (let marketItem of marketOfRoleList) {
                if (marketIdList[marketItem.MarketId]) {
                    newMarketItem = Object.assign({}, newMarketItem, { [marketItem.MarketId]: marketItem.HasPermission });
                }
            }

            return newMarketItem;
        }) || [];
    }

    _handleChange = (roleId, marketId, isChecked) => {
        const { gridData } = this.state;
        const indexItem = gridData.findIndex(item => item.RoleId === roleId);
        const updateItem = Object.assign({}, gridData[indexItem], { [marketId]: !isChecked });
        const gridDataUpdate = updateObjectInArray(gridData, indexItem, updateItem);

        this.setState({ gridData: gridDataUpdate });
    }

    _handleCancel = () => {
        this.setState({
            isEdit: false,
            gridData: this.props.gridData
        });
    }

    handleConvertData = () => {
        const { gridData } = this.state;
        const { marketList } = this.props;

        const data = gridData.map(item => {
            return marketList.map(market => {
                return {
                    RoleId: item.RoleId,
                    MarketId: market.MarketId,
                    HasPermission: item[market.MarketId]
                }
            })
        }).reduce((result, arrItem) => [...result, ...arrItem], [])

        return data;
    }

    _handleCloseNotificationPopup = (isConfirm) => {
        this.setState({ messagePopup: '' });

        if (isConfirm) {
            this._handleSave();
        }
    }

    _handleSubmit = () => {
        this.setState({ messagePopup: messages.AreYouSureUpdateLanguages, isConfirmPopup: true });
    }

    _handleSave = async () => {
        this.props.toggleLoader(true);

        const param = this.handleConvertData();
        const result = await this.props.updateMarketInfoList(param);

        if (result) {
            if (result.IsSuccessful) {
                this.setState({ messagePopup: "Update markets for roles are successful!", isConfirmPopup: false, isEdit: false });
            } else {
                this.setState({ messagePopup: result.Message, isConfirmPopup: false });
            }
        } else {
            this.setState({ messagePopup: messages.AnError, isConfirmPopup: false });
        }

        this.props.toggleLoader(false);
    }

    _renderHeader() {
        return (
            <Row key="theard">
                {
                    this._renderColumnHeader()
                }
            </Row>
        );
    }

    _renderColumnHeader() {
        const dataRows = [<Cell key={headerText.Role} header={true} colWidth={250} title={headerText.Role}>{headerText.Role}</Cell>];

        const { marketList } = this.props;

        if (marketList && marketList.length) {

            for (let market of marketList) {
                const marketName = market.MarketName;

                dataRows.push(<Cell key={marketName} header={true} colWidth={80}>{marketName}</Cell>);
            }
        }

        return dataRows;
    }

    _renderDataSection(dataList) {
        const dataRows = [];
        const { marketList } = this.props;

        if (dataList.length) {
            const { isEdit } = this.state;

            for (let dataItem of dataList) {
                dataRows.push(this._buildDataRow(dataItem, marketList, isEdit));
            }
        }
        else {
            dataRows.push(<Row>
                <Cell className="text-center" colSpan={1 + (marketList && marketList.length)}>{messages.NoInfo}</Cell>
            </Row>);
        }

        return dataRows;
    }

    _buildDataRow(dataItem, marketList, isEdit) {
        return (
            <Row key={`dataRow-${dataItem.RoleId}`}>
                {
                    this._renderColumnBody(dataItem, marketList, isEdit)
                }
            </Row>
        )
    }

    _renderColumnBody(dataItem, marketList, isEdit) {
        const dataRows = [<Cell key={dataItem.RoleName} title={dataItem.RoleName}>{dataItem.RoleName}</Cell>];

        if (marketList && marketList.length) {

            const isManagement = dataItem.RoleId === management;

            for (let item of marketList) {
                const isChecked = dataItem[item.MarketId];

                dataRows.push(
                    <Cell className="text-center" key={`dataCell-${item.MarketId}`}>
                        {
                            isEdit ? <input type="checkbox" checked={isChecked} disabled={isManagement} onChange={() => this._handleChange(dataItem.RoleId, item.MarketId, isChecked)} />
                                : (dataItem[item.MarketId] && <span className="oi oi-check text-primary"></span>)
                        }
                    </Cell>
                );
            }
        }

        return dataRows;
    }

    render() {
        const { hasWriteAdminRoleClaim } = this.props;
        const { isEdit, gridData, messagePopup, isConfirmPopup } = this.state;

        return gridData && (
            <div className={`data-content${hasWriteAdminRoleClaim ? '' : ' role-permission'}`}>
                {
                    hasWriteAdminRoleClaim && (
                        <div className="my-2">
                            {

                                isEdit ? (
                                    <Fragment>
                                        <Button outline type="button" color="danger" onClick={this._handleCancel}>Cancel<span className="oi oi-x ml-2"></span></Button>
                                        <Button outline type="button" className="ml-2" onClick={this._handleSubmit}>Save<span className="oi oi-task ml-2"></span></Button>
                                    </Fragment>
                                ) : (
                                        <Button outline type="button" onClick={() => this.setState({ isEdit: true })}> Edit < span className="oi oi-pencil ml-2"></span></Button>
                                    )
                            }
                        </div>
                    )
                }
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
                {
                    !!messagePopup && <NotificationPopup
                        message={messagePopup}
                        isConfirmPopup={isConfirmPopup}
                        onClose={this._handleCloseNotificationPopup}
                    />
                }
            </div>
        );
    }
}


function mapStateToProps({ gridData, roleList, marketIdList, marketList, hasWriteAdminRoleClaim }) {
    return Object.assign({}, { gridData, roleList, marketIdList, marketList, hasWriteAdminRoleClaim });
}

function mapDispatchToProps(dispatch) {
    return bindActionCreators({ getMarketListByRole, toggleLoader, setGridData, updateMarketInfoList }, dispatch);
}

export default connect(mapStateToProps, mapDispatchToProps)(LanguageComponent);