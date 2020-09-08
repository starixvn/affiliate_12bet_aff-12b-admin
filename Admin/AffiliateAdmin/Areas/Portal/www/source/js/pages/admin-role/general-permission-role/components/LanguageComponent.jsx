import React, { PureComponent } from 'react';
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';
import { Table, Row, Cell } from '@9softstudio/react-fixed-table-header';

import { headerText, globalResource, management } from '../libs/constants';
import { getMarketByRoleList, getMarketByUserList, toggleLoader, setGridData, updateMarketInfoList, setMessagePopup } from '../actions';
import { ControlButtons } from '../../common/components/ControlButtons';
import { updateObjectInArray } from '../../../../modules/dataHandler';

export class LanguageComponent extends PureComponent {
    constructor(props) {
        super(props);

        this.state = {
            isEdit: false,
            gridData: null,
        }
    }

    componentDidMount() {
        this.props.getMarketByUserList();

        this._handleGetMarketList();
    }

    _handleGetMarketList = async () => {
        this.props.toggleLoader(true);

        const marketList = await this.props.getMarketByRoleList();
        const newMarketList = this._convertMarketList(marketList);

        this.props.setGridData(newMarketList);
        this.setState({ gridData: newMarketList });

        this.props.toggleLoader(false);
    }

    _convertMarketList = (marketList) => {
        const { marketIdByUserList, roleList } = this.props;

        return marketIdByUserList && roleList.map(item => {
            const marketOfRoleList = marketList.filter(m => m.RoleId === item.RoleId);

            let newMarketItem = { ...item };

            for (let i = 0; i < marketOfRoleList.length; i++) {
                const marketItem = marketOfRoleList[i];

                if (marketIdByUserList[marketItem.MarketId]) {
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

    _handleSubmit = async () => {
        this.props.toggleLoader(true);

        const result = await this.props.updateMarketInfoList(this.state.gridData);

        if (result) {
            if (result.IsSuccessful) {
                this.props.setMessagePopup("Update Markets for Roles is successfully!");

                this.setState({ isEdit: false });
            }
            else {
                this.props.setMessagePopup(result.Message);
            }
        } else {
            this.props.setMessagePopup(globalResource.AnError);
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
        const dataRows = [<Cell header={true} colWidth={250} title={headerText.Role}>{headerText.Role}</Cell>];

        const { marketByUserList } = this.props;

        if (marketByUserList && marketByUserList.length) {

            for (let i = 0; i < marketByUserList.length; i++) {
                dataRows.push(<Cell header={true} colWidth={80}>{marketByUserList[i].MarketName}</Cell>);
            }
        }

        return dataRows;
    }

    _renderDataSection(dataList) {
        const dataRows = [];

        if (dataList && dataList.length) {
            const { isEdit } = this.state;
            const { marketByUserList } = this.props;

            for (let i = 0; i < dataList.length; i++) {
                const dataItem = dataList[i];
                dataRows.push(this._buildDataRow(dataItem, marketByUserList, isEdit));
            }
        }
        else {
            dataRows.push(<Row>
                <Cell className="text-center" colSpan="1">{globalResource.NoInfo}</Cell>
            </Row>);
        }

        return dataRows;
    }

    _buildDataRow(dataItem, marketByUserList, isEdit) {
        return (
            <Row key={`dataRow-${dataItem.RoleId}`}>
                {
                    this._renderColumnBody(dataItem, marketByUserList, isEdit)
                }
            </Row>
        )
    }

    _renderColumnBody(dataItem, marketByUserList, isEdit) {
        const dataRows = [<Cell title={dataItem.RoleName}>{dataItem.RoleName}</Cell>];

        if (marketByUserList && marketByUserList.length) {

            const isManagement = dataItem.RoleId === management;

            for (let i = 0; i < marketByUserList.length; i++) {
                const item = marketByUserList[i];
                const isChecked = dataItem[item.MarketId];

                dataRows.push(
                    <Cell className="text-center">
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
        const { isEdit, gridData } = this.state;

        return gridData && (
            <div className="data-content">
                <div className="my-2">
                    <ControlButtons
                        isEdit={isEdit}
                        handleEdit={() => this.setState({ isEdit: true })}
                        handleCancel={this._handleCancel}
                        handleSave={this._handleSubmit}
                    />
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


function mapStateToProps({ generalPermissionRole }) {
    const { gridData, roleList, marketIdByUserList, marketByUserList } = generalPermissionRole;

    return Object.assign({}, { gridData, roleList, marketIdByUserList, marketByUserList });
}

function mapDispatchToProps(dispatch) {
    return bindActionCreators({ getMarketByRoleList, getMarketByUserList, toggleLoader, setGridData, updateMarketInfoList, setMessagePopup }, dispatch);
}

export default connect(mapStateToProps, mapDispatchToProps)(LanguageComponent);