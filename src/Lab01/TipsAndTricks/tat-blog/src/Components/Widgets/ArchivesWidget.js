import { useState, useEffect } from 'react';
import ListGroup from 'react-bootstrap/ListGroup';
import { Link } from 'react-router-dom';
import { getArchivesWidget } from '../../Services/Widgets';
import { getMonth } from '../../Utils/Utils';

const ArchivesWidget = () => {
    const [monthList, setMonthList] = useState([]);

    useEffect(() => {
        getArchivesWidget().then(data => {
            if (data)
                setMonthList(data);
            else
                setMonthList([]);
        });
    }, [])

    return (
        <div className='mb-4'>
            <h3 className='text-success mb-2'>
                Danh sách 12 tháng gần nhất
            </h3>
            {monthList.length > 0 &&
                <ListGroup>
                    {monthList.map((item, index) => {
                      return (
                        <ListGroup.Item key={index}>
                            <Link to={`/blog/month?${getMonth(item.month)}`}
                              title={item.description}
                              key={index}>
                                {`${getMonth(item.month)}`}
                              <span>&nbsp;({item.postCount})</span>
                            </Link>
                        </ListGroup.Item>
                      );
                    })}
                </ListGroup>
            }
        </div>
    );
}
export default ArchivesWidget;