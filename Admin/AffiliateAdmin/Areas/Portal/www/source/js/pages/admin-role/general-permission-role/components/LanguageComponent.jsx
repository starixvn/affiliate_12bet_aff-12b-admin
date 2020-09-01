import React, { PureComponent, Fragment } from 'react';
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';
import { Table, Row, Cell } from '@9softstudio/react-fixed-table-header';

import { headerText, globalResource } from '../libs/constants';
import { getMarketList, toggleLoader, setGridData } from '../actions';
import { ControlButtons } from '../../common/components/ControlButtons';

const languageList = [
    { MarketId: "en-GB", MarketName: 'English' },
    { MarketId: "vi-VN", MarketName: 'English (Europe)' }
];

const sortLanguages = {
    "en-GB": "en-GB",
    "vi-VN": "vi-VN"
};

const roleList = [
    { RoleId: 1, RoleName: 'Management', "en-GB": true },
    { RoleId: 2, RoleName: 'Exe. Settlement', "en-GB": true },
    { RoleId: 3, RoleName: 'Exe. Risk Team', "en-GB": true },
    { RoleId: 4, RoleName: 'Exe. Marketing', "en-GB": true },
    { RoleId: 5, RoleName: 'Asst. Marketing', "en-GB": true }
]

export class LanguageComponent extends PureComponent {
    constructor(props) {
        super(props);

        this.state = {
            isEdit: false,
            gridData: null,
        }
    }

    componentDidMount() {

    }

    _handleGetMarketList = () => {
        this.props.toggleLoader(true);

        const marketList = this.props.getMarketList();
        const newMarketList = this._convertMarketList(marketList, roleList);

        this.setGridData(newMarketList);
        this.setState({ gridData: newMarketList });

        this.props.toggleLoader(false);
    }

    _convertMarketList = (marketList, roleList) => {

        return roleList.map(item => {
            const marketOfRoleList = marketList.filter(m => m.RoleId === item.RoleId);
            const newMarketItem = { ...item };

            for (let i = 0; i < marketOfRoleList.length; i++) {
                const marketItem = marketOfRoleList[i];

                if (sortLanguages[marketItem.MarketId]) {
                    newMarketItem = Object.assign({}, newMarketItem, { [marketItem.MarketId]: marketItem.HasPermission });
                }
            }

            return newMarketItem;
        })
    }

    _handleChange = (roleId, marketId, isChecked) => {
        const { gridData } = this.state;
        const indexItem = gridData.findIndex(item => item.RoleId === roleId);
        const updateItem = Object.assign({}, gridData[indexItem], { [marketId]: isChecked });
        const gridDataUpdate = updateObjectInArray(gridData, indexItem, updateItem)

        this.setState({ gridData: gridDataUpdate });
    }

    _renderHeader() {
        return (
            <Row key="theard">
                {
                    this._renderColumnHeader(languageList)
                }
            </Row>
        );
    }

    _renderColumnHeader(dataList) {
        const dataRows = [<Cell header={true} colWidth={250} title={headerText.Role}>{headerText.Role}</Cell>];

        if (dataList && dataList.length) {

            for (let i = 0; i < dataList.length; i++) {
                dataRows.push(<Cell header={true} colWidth={80}>{dataList[i].MarketName}</Cell>);
            }
        }

        return dataRows;
    }

    _renderDataSection(dataList) {
        const dataRows = [];

        if (dataList && dataList.length) {
            const { isEdit } = this.state;

            for (let i = 0; i < dataList.length; i++) {
                const dataItem = dataList[i];
                dataRows.push(this._buildDataRow(dataItem, isEdit));
            }
        }
        else {
            dataRows.push(<Row>
                <Cell className="text-center" colSpan={languageList.length}>{globalResource.NoInfo}</Cell>
            </Row>);
        }

        return dataRows;
    }

    _buildDataRow(dataItem, isEdit) {
        return (
            <Row key={`dataRow-${dataItem.RoleId}`}>
                {
                    this._renderColumnBody(dataItem, isEdit)
                }
            </Row>
        )
    }

    _renderColumnBody(dataItem, isEdit) {
        const dataRows = [<Cell title={dataItem.RoleName}>{dataItem.RoleName}</Cell>];

        if (languageList && languageList.length) {

            for (let i = 0; i < languageList.length; i++) {
                const item = languageList[i];
                const isChecked = dataItem[item.MarketId];

                dataRows.push(
                    <Cell className="text-center">
                        {
                            isEdit ? <input type="checkbox" checked={isChecked} onChange={() => this._handleChange(dataItem.RoleId, item.MarketId, !isChecked)} />
                                : (isChecked && <span className="oi oi-check text-primary"></span>)
                        }
                    </Cell>
                );
            }
        }

        return dataRows;
    }

    render() {
        const { gridData } = this.props;
        const { isEdit } = this.state;

        return gridData && (
            <div className="data-content">
                <div className="my-2">
                    <ControlButtons
                        isEdit={isEdit}
                        handleEdit={() => this.setState({ isEdit: true })}
                        handleCancel={() => this.setState({ isEdit: false })}
                        handleSave={this._handleSubmit}
                    />
                </div>
                <div className="table-responsive table-striped my-2">
                    <Table
                        minWidth={1000}
                        autoWidth={true}
                        bodyHeight={300}
                        header={this._renderHeader()}
                        body={this._renderDataSection(roleList)}
                        containerPadding={52}
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
    return bindActionCreators({ getMarketList, toggleLoader, setGridData }, dispatch);
}

export default connect(mapStateToProps, mapDispatchToProps)(LanguageComponent);