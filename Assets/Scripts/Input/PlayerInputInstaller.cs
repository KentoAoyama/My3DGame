using UnityEngine;
using Zenject;

public class PlayerInputInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container
            .Bind<IInputProvider>() // IInputProvider�ɑ΂���
            .To<PlayerInputProvider>() // PlayerInputProvider�̃C���X�^���X�𒍓�����
            .AsCached(); // ���p�ł���C���X�^���X�����łɂ���΂���Ŏ��s
    }
}