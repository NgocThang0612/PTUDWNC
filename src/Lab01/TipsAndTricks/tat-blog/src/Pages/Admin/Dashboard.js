import React, { useEffect, useState } from 'react';
import Table from 'react-bootstrap/Table';
import { Link, useParams } from 'react-router-dom';
import { getDashboard } from '../../Services/BlogReponsitory';
import Loading from '../../Components/Loading';

const Dashboard = () => {
    const [dashboard, setDashboardList] = useState([]),
        [isVisibleLoading, setIsVisibleLoading] = useState(true);

        useEffect(() => {
            document.title = 'Danh sách Dashboard';
         
            getDashboard().then(data => {
                    console.log("data:")
                    console.log(data)
                    if (data)
                        setDashboardList(data);
                    else
                        setDashboardList([]);
                    setIsVisibleLoading(false);
                });
        }, []);

    return (
        <>
            <h1>Danh sách Dashboard </h1>
            {isVisibleLoading ? <Loading /> :
                <div style={{display: "inline-flex"}}>
                    <div class="card text-white bg-primary mb-3" style={{maxWidth: "16rem"}}>
                        <div class="card-header">Tổng số bài viết</div>
                        <div class="card-body">
                            <h5 class="card-title">{dashboard.totalPost}</h5>
                        </div>
                    </div>
                    <div class="card text-white bg-secondary mb-3" style={{maxWidth: "16rem"}}>
                        <div class="card-header">Số bài viết chưa xuất bản</div>
                        <div class="card-body">
                            <h5 class="card-title">{dashboard.totalUnpublishedPost}</h5>
                            
                        </div>
                    </div>
                    <div class="card text-white bg-success mb-3" style={{maxWidth: "16rem"}}>
                        <div class="card-header">Số lượng chủ đề</div>
                        <div class="card-body">
                            <h5 class="card-title">{dashboard.totalCategorie}</h5>
                            
                        </div>
                    </div>
                    <div class="card text-white bg-danger mb-3" style={{maxWidth: "16rem"}}>
                        <div class="card-header">Số lượng tác giả</div>
                        <div class="card-body">
                            <h5 class="card-title">{dashboard.totalAuthor}</h5>
                        
                        </div>
                    </div>
                    <div class="card text-white bg-warning mb-3" style={{maxWidth: "16rem"}}>
                        <div class="card-header">Số lượng bình luận đang chờ phê duyệt</div>
                        <div class="card-body">
                            <h5 class="card-title">{dashboard.totalUnapprovedComment}</h5>
                            
                        </div>
                    </div>
                    <div class="card text-white bg-info mb-3" style={{maxWidth: "16rem"}}>
                        <div class="card-header">Số lượng người theo dõi</div>
                        <div class="card-body">
                            <h5 class="card-title">{dashboard.totalSubscriber}</h5>
                            
                        </div>
                    </div>
                    <div class="card bg-light mb-3" style={{maxWidth: "16rem"}}>
                        <div class="card-header">Số lượng người mới theo dõi trong ngày</div>
                        <div class="card-body">
                            <h5 class="card-title">{dashboard.totalNewSubscriberToday}</h5>
                        </div>
                    </div>
                </div>
            
            }
        </>
    );
}


export default Dashboard;