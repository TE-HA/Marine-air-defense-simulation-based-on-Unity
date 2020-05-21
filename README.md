# Marine-air-defense-simulation-based-on-Unity3D
Marine air defense simulation based on Unity in 2020

####begin to log####

添加堆结构对任务进行优先级调度，堆调整的key为威胁评估系统评价出来的优先级（值），但需要具体实现。
-2020.4.16

为打击资源、制导资源、探测资源添加辅助射线，可见图4.17，同时优化部分代码（数据库模块、内存管理），游戏运行更加流畅稳定。
-2020.4.17

实现任务堆的创建，调整，弹出任务级别最高的任务节点并分配制导资源、打击资源进行来接，对于来袭目标有三种可能情况：1.新目标出现（威胁程度更高），因为堆的上浮机制可迅速达到触发条件；2.拦截目标失败，则将该目标的现有威胁程度x2,然后直接加入任务堆，跟情况1类似；3.拦截目标成功，则立即释放制导资源和预警资源，打击资源在拦截导弹发射两秒之后自动释放。
对部分流程进行重新设计，使之更符合实战条件之下的战术过程。
经过压力测试，在六艘舰艇，每艘舰艇打击资源1，制导资源3，预警资源3的情况下，在敌方4分钟内连续攻击50次，我方打击敌军100次的情况下，我方目标送受损率不足5%，优秀！部分运行截图可如4.18
-2020.4.18

制定规则：
1.敌军出现进入防空识别区时即可开始预警
2.随时间增加敌军目标（飞机、导弹）的威胁程度
3.各目标威胁程度>系统设定阈值时触发拦截机制，分配制导资源x2，打击资源x1
4.预警资源在敌军目标消失前一直保持跟踪，制导资源在敌军被击毁之前一直保持跟踪，打击资源在发射2s后释放
5.拦截导弹失败后导弹威胁值x2，如果导弹距离不足3000m时威胁值直接+800（这样可以以最大速度重新拦截目标）
6.maybe more
2020.4.19

1.研究目的和意义
1.1舰艇编队联合防空场景仿真
1.2事件机制
1.3国内外研究现状
-2020.4.20

添加功能:
1.显示当前防空系统的资源消耗量，可见图4.21
2.对战斗产生的数据进行读取，便于后续对于防空效能的分析
3.优化防空算法，使系统导弹拦截成功率提升10%，目前防空效果敌军50次攻击，造成我方被击中概率为2%以下，且可以出现无一击中的现象
4.界面ui需要进一步完善
2020.4.21

开始撰写论文：
完成研究背景意义和作战过程作图，同时完善了部分细节
2020.4.23

完成各模块功能作图，完善论文体系结构，为后期填充内容打下基础
2020.4.24

今天咸鱼了，早上完成个petri图，没了
2020.4.25

完善系统所使用的工具MySql、C#、3D MAX、etc，并构思之后的论文
2020.4.26

1.代码重构，主要针对数据库部分
2.对整个系统的数据流程进行梳理，绘制出数据流图
3.对数据库重新设计，绘制出er图
4.对后续战后分析所需要的数据进行初步探索，绘制直线图讨论不同来袭数量、不同单发拦截概率等对于舰艇编队的存活概率
2020.4.27

完善论文防空作战过程表达章节，主讲系统防空作战流程以及系统防空作战关键算法，并完成阶段性报告5.1，在论文细节方面需要插入公式显得高大上，不能让别人轻易看懂。
2020.4.28

重大版本更新！！！
1.制定新规则
a.系统所拦截阈值并非一成不变，而是根据系统所受攻击频率而弹性变化，当制导资源使用量超过80%时，系统会提高拦截阈值，也就是说，低于该阈值的且未分配抗击资源的目标将会被放弃，低于该阈值但已分配抗击资源的目标将会回收抗击资源，放弃拦截，同理，当系统制导资源使用量不足50%时，系统会降低拦截阈值
b.敌军系统攻击战术改变，分为不同轮次，每一轮次有一定数量的敌机和导弹
c.达到阈值加入任务堆中的目标，指控系统会设定一个任务缓冲时间，以防止系统拦截阈值变大而造成资源浪费，在达到缓冲时间之后若无更高威胁抢占资源，则会执行该任务
d.为应对饱和攻击，敌机在被击中之后立即释放制导资源，预警资源不释放
2.增加新功能（可见截图4.29）
a.图形化演示任务堆核心算法过程
b.重写制导系统功能
c.优化预警功能
3.优化UI设计
（今日代码增加量为1076行）
2020.4.29

论文码字，同时取消论文版本的网页更新
2020.4.30

1.为舰船添加移动规避的躲避导弹方式，结果发现他们那些导弹基本没戏，完全打不到
2.将打击、预警、制导三类资源区分开，独立显示，可详见图5.1
3.论文丰富内容
2020.5.1

1.主要搭建评估系统，制定了指标，但感觉不是很完备
2.整个下午解决一个天坑BUG，该BUG曾导致评估系统直接无法工作，具体原因是因为阈值身高，而有些导弹无法得到资源便会直接打击舰船，造成数据库没有此导弹数据，真是天坑啊！！
2020.5.2

1.对不同敌方输入条件进行数据仿真，共仿真大概30组数据，取平均制表，并作出图
2.分析折线图的原因
3.感觉需要优化威胁值机制
2020.5.3

每天都在解决超级bug中度过
2020.5.4

写论文，加了一些公式
2020.5.6

写论文，加了一些图片，调整了一丢丢格式
2020.5.7

无，老师怎么还不开会讲事儿
2020.5.8

论文查重率好高，明天改一下
2020.5.14

修改论文，发现网页copy比较多，因此降重之路尤为艰难
2020.5.15

修改论文，重复率降低为14%，但仍需完善部分内容
2020.5.16

修改论文，对图表公式进行编号，同时完善部分语句，初步定稿，但希望明天能够完善总结展望部分，引入强化学习概念。
2020.5.17

论文目前定稿，等待后期查漏补缺
2020.5.18

啥也没干
2020.5.19

啥也没干_2
2020.5.20

啥也没干_3
2020.5.21