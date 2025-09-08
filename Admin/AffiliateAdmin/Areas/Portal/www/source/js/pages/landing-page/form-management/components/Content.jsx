import React, { useEffect } from 'react';
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';
import { Table, Row, Cell } from '@9softstudio/react-fixed-table-header';
import { Button } from 'reactstrap';

import { globalResource } from '../../../../modules/globalResource';
import { headerText } from '../libs/constants';
import { maxWidthHeight } from '../../libs/constants';
import { setSelectedMediaId, setPagingOption, fetchData, setCheckedForms, setShouldResetScrollPosition, setParam, setFormDetailUrl } from '../actions';
import { resizeWidthHeight } from '../../../../modules/utils';

export const Content = (props) => {
    useEffect(() => {
        const { param } = props;

        param && props.fetchData(param);
    }, [props.param]);

    const _onCheckItem = (isChecked, mediaId) => {
        const newCheckedForms = Object.assign({}, props.checkedForms, { [mediaId]: isChecked });

        props.setShouldResetScrollPosition(false);
        props.setCheckedForms(newCheckedForms);
    }

    const _onDeleteItem = (mediaId) => {
        // Handle single item deletion
        if (window.confirm('Are you sure you want to delete this item?')) {
            // Add delete logic here - this would typically call a delete action
            console.log('Delete item with ID:', mediaId);
            // props.deleteItem(mediaId); // Uncomment when delete action is available
        }
    }

    const _onToggleActiveStatus = async (mediaId, currentStatus) => {
        try {
            const newStatus = !currentStatus;
            const result = await props.toggleFormStatus(mediaId, newStatus);
            
            if (result && result.IsSuccessful) {
                // Refresh the data to show updated form status
                props.fetchData(props.param);
            } else {
                alert(result?.Message || 'Failed to update form status');
            }
        } catch (error) {
            console.error('Error toggling form status:', error);
            alert('An error occurred while updating the form status');
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
                <Cell header={true} colWidth={15}></Cell>
                <Cell header={true} colWidth={15} title={headerText.Index}>{headerText.Index}</Cell>
                <Cell header={true} colWidth={100} title={headerText.FormName}>{headerText.FormName}</Cell>
                <Cell header={true} colWidth={100} title={headerText.PreviewImageName}>{headerText.PreviewImageName}</Cell>
                <Cell header={true} colWidth={50} title={headerText.Demensions}>{headerText.Demensions}</Cell>
                <Cell header={true} colWidth={200} title={headerText.PreviewImage}>{headerText.PreviewImage}</Cell>
                <Cell header={true} colWidth={20} title={headerText.Active}>{headerText.Active}</Cell>
                <Cell header={true} colWidth={20} title={headerText.Action}>{headerText.Action}</Cell>
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
            dataRows.push(<Row key="no-info">
                <Cell className="text-center" colSpan="8">{globalResource.NoInfo}</Cell>
            </Row>);
        }

        return dataRows;
    }

    function _buildDataRow(dataItem, index) {
        const mediaId = dataItem.MediaId;
        const formName = dataItem.FormName;
        const fileName = dataItem.FileName;
        const src = `${props.bannerRootUrl}/${fileName}?id=${dataItem.LastUpdateText}`;
        const size = dataItem.Size;
        const strSize = size.split('x');
        const realWidth = strSize[0];
        const realHeight = strSize[1];
        const isValidWithHeight = realWidth < maxWidthHeight && realHeight < maxWidthHeight;
        const resizeResult = !isValidWithHeight && resizeWidthHeight(realWidth, realHeight);

        console.log(props.checkedForms);

        return (
            <Row key={`dataRow-${index}`}>
                <Cell className="text-center">
                    <input type="checkbox" checked={props.checkedForms[mediaId] || false}
                        onChange={(e) => _onCheckItem(e.target.checked, mediaId)} />
                </Cell>
                <Cell className="text-center" title={index}>{index}</Cell>
                <Cell className="text-center text-break" title={formName}>{formName}</Cell>
                <Cell className="text-center text-break" title={fileName}>{fileName}</Cell>
                <Cell className="text-center text-break" title={size}>{size}</Cell>
                <Cell className="text-center">
                    {
                        isValidWithHeight ? (
                            <img id={`img${mediaId}`} src={src} alt="Preview Image" height={realHeight} width={realWidth} />
                        ) : (
                                <a href="javascript:;" id={`link${mediaId}`} onClick={() => props.setFormDetailUrl({ src, mediaId, width: realWidth, height: realHeight })}>
                                    <img id={`img${mediaId}`} src={src} alt="Preview Image" height={resizeResult.height} width={resizeResult.width} />
                                </a>
                            )
                    }
                </Cell>
                <Cell className="text-center">
                    <input
                            type="checkbox"
                            id={`activeSwitch${mediaId}`}
                            checked={dataItem.IsActive}
                            onChange={() => _onToggleActiveStatus(mediaId, dataItem.IsActive)}
                        />
                </Cell>
                <Cell className="text-center">
                    <Button color="link" onClick={() => props.setSelectedMediaId(mediaId)} title="Edit">
                        <span className="oi oi-pencil text-secondary"></span>
                    </Button>
                    <Button color="link" onClick={() => _onDeleteItem(mediaId)} title="Delete" className="ml-1">
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

function mapStateToProps({ pagingOption, checkedForms, shouldResetScrollPosition, gridData, param, bannerRootUrl }) {
    return Object.assign({}, { pagingOption, checkedForms, shouldResetScrollPosition, gridData, param, bannerRootUrl });
}

function mapDispatchToProps(dispatch) {
    return bindActionCreators({ setSelectedMediaId, fetchData, setPagingOption, setCheckedForms, setShouldResetScrollPosition, setParam, setFormDetailUrl }, dispatch);
}

export default connect(mapStateToProps, mapDispatchToProps)(React.memo(Content));