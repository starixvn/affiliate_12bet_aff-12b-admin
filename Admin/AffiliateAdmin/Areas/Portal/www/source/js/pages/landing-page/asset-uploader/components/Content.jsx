import React, { useEffect } from 'react';
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';
import { Table, Row, Cell } from '@9softstudio/react-fixed-table-header';
import { Button } from 'reactstrap';

import { resizeWidthHeight } from '../libs/utils';
import { globalResource } from '../../../../modules/globalResource';
import { headerText, maxWidthHeight } from '../libs/constants';
import { setSelectedAssetId, setPagingOption, fetchData, setCheckedAssets, setShouldResetScrollPosition, setParam, setAssetDetailUrl } from '../actions';

export const Content = (props) => {

    useEffect(() => {
        const { param } = props;

        param && props.fetchData(param);
    }, [props.param]);

    const _onCheckItem = (isChecked, assetId) => {
        const newCheckedAssets = Object.assign({}, props.checkedAssets, { [assetId]: isChecked });

        props.setShouldResetScrollPosition(false);
        props.setCheckedAssets(newCheckedAssets);
    }

    const _onDeleteItem = (assetId) => {
        // Handle single item deletion
        if (window.confirm('Are you sure you want to delete this item?')) {
            // Add delete logic here - this would typically call a delete action
            console.log('Delete item with ID:', assetId);
            // props.deleteItem(assetId); // Uncomment when delete action is available
        }
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
                <Cell header={true} colWidth={60} title={headerText.Index}>{headerText.Index}</Cell>
                <Cell header={true} colWidth={200} title={headerText.MediaName}>{headerText.MediaName}</Cell>
                <Cell header={true} colWidth={200} title={headerText.FileName}>{headerText.FileName}</Cell>
                <Cell header={true} colWidth={150} title={headerText.Module}>{headerText.Module}</Cell>
                <Cell header={true} colWidth={120} title={headerText.Dimensions}>{headerText.Dimensions}</Cell>
                <Cell header={true} colWidth={300} title={headerText.Images}>{headerText.Images}</Cell>
                <Cell header={true} colWidth={120} title={headerText.Action}>{headerText.Action}</Cell>
            </Row>
        );
    }

    function _renderDataSection(dataList) {
        const dataRows = [];

        if (dataList.length) {
            const { pagingOption } = props;
            const startIndex = (pagingOption.PageIndex - 1) * pagingOption.PageSize + 1;

            for (let i = 0; i < dataList.length; i++) {
                const dataItem = dataList[i];
                dataRows.push(_buildDataRow(dataItem, i + startIndex));
            }
        }
        else {
            dataRows.push(<Row key="no-info">
                <Cell className="text-center" colSpan="8">{globalResource.NoInfo}</Cell>
            </Row>);
        }

        return dataRows;
    }

    function _buildDataRow(dataItem, index) {
        const assetId = dataItem.AssetId;
        const fileName = dataItem.FileName;
        const mediaName = dataItem.MediaName || fileName; // Use MediaName if available, fallback to FileName
        const module = dataItem.Module || dataItem.Category || ''; // Use Module if available, fallback to Category
        const src = `${props.bannerRootUrl}/${fileName}?id=${dataItem.LastUpdateText}`;
        const size = dataItem.Size;
        const strSize = size.split('x');
        const realWidth = strSize[0];
        const realHeight = strSize[1];
        const isValidWithHeight = realWidth < maxWidthHeight && realHeight < maxWidthHeight;
        const resizeResult = !isValidWithHeight && resizeWidthHeight(realWidth, realHeight);

        return (
            <Row key={`dataRow-${index}`}>
                <Cell className="text-center">
                    <input type="checkbox" checked={props.checkedAssets[assetId] || false}
                        onChange={(e) => _onCheckItem(e.target.checked, assetId)} />
                </Cell>
                <Cell className="text-center" title={index}>{index}</Cell>
                <Cell className="text-center text-break" title={mediaName}>{mediaName}</Cell>
                <Cell className="text-center text-break" title={fileName}>{fileName}</Cell>
                <Cell className="text-center text-break" title={module}>{module}</Cell>
                <Cell className="text-center text-break" title={size}>{size}</Cell>
                <Cell className="text-center">
                    {
                        isValidWithHeight ? (
                            <img id={`img${assetId}`} src={src} alt="Banner" height={realHeight} width={realWidth} />
                        ) : (
                                <a href="javascript:;" id={`link${assetId}`} onClick={() => props.setAssetDetailUrl({ src, assetId, width: realWidth, height: realHeight })}>
                                    <img id={`img${assetId}`} src={src} alt="Banner" height={resizeResult.height} width={resizeResult.width} />
                                </a>
                            )
                    }
                </Cell>
                <Cell className="text-center">
                    <Button color="link" onClick={() => props.setSelectedAssetId(assetId)} title="Edit">
                        <span className="oi oi-pencil text-secondary"></span>
                    </Button>
                    <Button color="link" onClick={() => _onDeleteItem(assetId)} title="Delete" className="ml-1">
                        <span className="oi oi-trash text-danger"></span>
                    </Button>
                </Cell>
            </Row>
        )
    }

    return props.gridData && (
        <div className="data-content">
            <div className="banner-list-content table-responsive table-striped my-2">
                <Table
                    minWidth={1190}
                    autoWidth={true}
                    bodyHeight={300}
                    header={_renderHeader()}
                    body={_renderDataSection(props.gridData)}
                    containerPadding={20}
                    isPaging={true}
                    pageOption={props.pagingOption}
                    onPaging={_handlePaging}
                    shouldResetScrollPosition={props.shouldResetScrollPosition}
                />
            </div>
        </div>
    );
}

function mapStateToProps({ pagingOption, param, checkedAssets, assetRootUrl, shouldResetScrollPosition, gridData }) {
    return Object.assign({}, { pagingOption, param, checkedAssets, assetRootUrl, shouldResetScrollPosition, gridData });
}

function mapDispatchToProps(dispatch) {
    return bindActionCreators({ setSelectedAssetId, fetchData, setPagingOption, setCheckedAssets, setShouldResetScrollPosition, setParam, setAssetDetailUrl }, dispatch);
}

export default connect(mapStateToProps, mapDispatchToProps)(React.memo(Content));