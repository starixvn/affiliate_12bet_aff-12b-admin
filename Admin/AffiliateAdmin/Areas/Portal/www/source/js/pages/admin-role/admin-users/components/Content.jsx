import React, { useEffect } from 'react';
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';
import { Table, Row, Cell } from '@9softstudio/react-fixed-table-header';
import { Button } from 'reactstrap';

import { htmlDecode } from '../../../../modules/htmlHelper';
import { resource } from '../libs/constants';
import { resolveClientUrl } from '../../../../modules/utils';
import { setPagingOption, fetchData, setCheckedUsers, setParam } from '../actions';

export const Content = (props) => {

    useEffect(() => {
        const { param } = props;
        !!param && props.fetchData(param);
    }, [props.param]);

    const _onCheckItem = (isChecked, userId) => {
        const newCheckedUsers = Object.assign({}, props.checkedUsers, { [userId]: isChecked });

        props.setCheckedUsers(newCheckedUsers);
    }

    const _handlePaging = (pageIndex, pageSize) => {
        const newPaging = {
            PageIndex: pageIndex,
            PageSize: pageSize
        };
        const newParam = Object.assign({}, props.param, newPaging);

        props.setPagingOption(newPaging);
        props.setParam(newParam);
    };

    function _renderHeader() {
        return (
            <Row key="thead">
                <Cell header={true} colWidth={40}></Cell>
                <Cell header={true} colWidth={40} title={resource.Index}>{resource.Index}</Cell>
                <Cell header={true} colWidth={250} title={resource.UserName}>{resource.UserName}</Cell>
                <Cell header={true} colWidth={250} title={resource.LoggedIn}>{resource.LoggedIn}</Cell>
                <Cell header={true} colWidth={250} title={resource.LastRequest}>{resource.LastRequest}</Cell>
                <Cell header={true} colWidth={250} title={resource.IP}>{resource.IP}</Cell>
                <Cell header={true} colWidth={250} title={resource.Role}>{resource.Role}</Cell>
            </Row>
        );
    }

    function _renderDataSection(dataList) {
        const dataRows = [];

        if (dataList && dataList.length) {
            const { pagingOption } = props;
            const startIndex = (pagingOption.PageIndex - 1) * pagingOption.PageSize + 1;

            for (let i = 0; i < dataList.length; i++) {
                const dataItem = dataList[i];
                dataRows.push(_buildDataRow(dataItem, i + startIndex));
            }
        }
        else {
            dataRows.push(<Row>
                <Cell className="text-center" colSpan="8">{resource.NoInfo}</Cell>
            </Row>);
        }

        return dataRows;
    }

    function _buildDataRow(dataItem, index) {
        const userName = htmlDecode(dataItem.UserName);
        const ipAddress = '122.53.154.194';
        const url = `http://anonym.to/?http://legacytools.dnsstuff.com/tools/ipall.ch?tool_id=67&token=&toolhandler_redirect=0&ip=${ipAddress}&j=1`;

        return (
            <Row key={`dataRow-${index}`}>
                <Cell className="text-center">
                    <input type="checkbox" checked={props.checkedUsers[dataItem.UserId]}
                        onChange={(e) => _onCheckItem(e.target.checked, dataItem.UserId)} />
                </Cell>
                <Cell className="text-center" title={index}>{index}</Cell>
                <Cell className="text-center" title={resource.userName}>
                    <a href={resolveClientUrl(`Portal/AdminInfo`)} target="_top">
                        {userName}
                    </a>
                </Cell>
                <Cell className="text-center" title={dataItem.CreatedDateText}>{dataItem.CreatedDateText}</Cell>
                <Cell className="text-center" title={dataItem.CreatedDateText}>{dataItem.CreatedDateText}</Cell>
                <Cell className="text-center" title={ipAddress}>
                    <a href={url} target="_tagert">
                        {ipAddress}
                    </a>
                </Cell>
                <Cell className="text-center" title={dataItem.GroupName}>{dataItem.GroupName}</Cell>
            </Row>
        )
    }

    return props.gridData && (
        <div className="data-content">
            <div className="admin-user-content table-responsive table-striped my-2">
                <Table
                    minWidth={1000}
                    autoWidth={true}
                    bodyHeight={300}
                    header={_renderHeader()}
                    body={_renderDataSection(props.gridData)}
                    containerPadding={20}
                    isPaging={true}
                    pageOption={props.pagingOption}
                    onPaging={_handlePaging}
                />
            </div>
        </div>
    );
}


function mapStateToProps({ pagingOption, param, checkedUsers, gridData }) {
    return Object.assign({}, { pagingOption, param, checkedUsers, gridData });
}

function mapDispatchToProps(dispatch) {
    return bindActionCreators({ fetchData, setPagingOption, setCheckedUsers, setParam }, dispatch);
}

export default connect(mapStateToProps, mapDispatchToProps)(React.memo(Content));